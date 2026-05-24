#!/usr/bin/env bash

set -euo pipefail

if [[ -z "${ILSPY_DIR:-}" ]]; then
  echo "Error: ILSPY_DIR is not set."
  exit 1
fi

if [[ ! -d "$ILSPY_DIR" ]]; then
  echo "Error: ILSPY_DIR does not exist: $ILSPY_DIR"
  exit 1
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
WORKSPACE_DIR="$(cd "$SCRIPT_DIR/../../.." && pwd)"
DEPS_DIR="$WORKSPACE_DIR/modules/gdsdecomp/godot-mono-decomp/dependencies"
CSPROJ_PATH="$WORKSPACE_DIR/modules/gdsdecomp/godot-mono-decomp/GodotMonoDecomp/GodotMonoDecomp.csproj"

mkdir -p "$DEPS_DIR"

echo "Packing ILSpy packages into: $DEPS_DIR"
(
  cd "$ILSPY_DIR"
  dotnet pack -c Release ICSharpCode.Decompiler -o "$DEPS_DIR"
  dotnet pack -c Release ICSharpCode.ILSpyX -o "$DEPS_DIR"
)

latest_package_version() {
  local package_name="$1"
  local newest

  newest="$(
    ls -t "$DEPS_DIR/${package_name}."*.nupkg 2>/dev/null \
      | while IFS= read -r path; do
          [[ "$path" == *.snupkg ]] && continue
          printf '%s\n' "$path"
        done \
      | head -n 1
  )"

  if [[ -z "$newest" ]]; then
    echo "Error: Could not find packed package for $package_name in $DEPS_DIR"
    exit 1
  fi

  local filename version
  filename="${newest##*/}"
  version="${filename#${package_name}.}"
  version="${version%.nupkg}"
  printf '%s\n' "$version"
}

prune_older_packages() {
  local package_name="$1"
  local keep_version="$2"

  shopt -s nullglob
  local files=(
    "$DEPS_DIR/${package_name}."*.nupkg
    "$DEPS_DIR/${package_name}."*.snupkg
  )
  shopt -u nullglob

  for file in "${files[@]}"; do
    case "$file" in
      "$DEPS_DIR/${package_name}.${keep_version}.nupkg" | "$DEPS_DIR/${package_name}.${keep_version}.snupkg")
        ;;
      *)
        rm -f "$file"
        ;;
    esac
  done
}

decompiler_version="$(latest_package_version "ICSharpCode.Decompiler")"
ilspyx_version="$(latest_package_version "ICSharpCode.ILSpyX")"

prune_older_packages "ICSharpCode.Decompiler" "$decompiler_version"
prune_older_packages "ICSharpCode.ILSpyX" "$ilspyx_version"

if [[ ! -f "$CSPROJ_PATH" ]]; then
  echo "Error: csproj not found: $CSPROJ_PATH"
  exit 1
fi

# Remove existing Decompiler/ILSpyX references (active or commented), then insert updated package refs.
sed -i.bak '/PackageReference Include="ICSharpCode\.Decompiler"/d' "$CSPROJ_PATH"
sed -i.bak '/PackageReference Include="ICSharpCode\.ILSpyX"/d' "$CSPROJ_PATH"
sed -i.bak '/ProjectReference Include=".*ICSharpCode\.Decompiler\.csproj"/d' "$CSPROJ_PATH"
sed -i.bak '/ProjectReference Include=".*ICSharpCode\.ILSpyX\.csproj"/d' "$CSPROJ_PATH"

sed -i.bak "/<PackageReference Include=\"NuGet.Packaging\"/i\\
      <PackageReference Include=\"ICSharpCode.Decompiler\" Version=\"$decompiler_version\" />\\
      <PackageReference Include=\"ICSharpCode.ILSpyX\" Version=\"$ilspyx_version\" />
" "$CSPROJ_PATH"

rm -f "$CSPROJ_PATH.bak"

echo "Updated $CSPROJ_PATH"
echo " - ICSharpCode.Decompiler: $decompiler_version"
echo " - ICSharpCode.ILSpyX: $ilspyx_version"
