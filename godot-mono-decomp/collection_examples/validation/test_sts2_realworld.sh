#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
REPO_DIR="$(cd "$SCRIPT_DIR/../.." && pwd)"
CLI_PROJECT="$REPO_DIR/GodotMonoDecompCLI/GodotMonoDecompCLI.csproj"

DEFAULT_DLL_PATH="/Users/nikita/Library/Application Support/Steam/steamapps/common/Slay the Spire 2/SlayTheSpire2.app/Contents/Resources/data_sts2_macos_arm64/sts2.dll"
DEFAULT_OUTPUT_ROOT="$SCRIPT_DIR/realworld_test_out"

if [[ "${1:-}" == "-h" || "${1:-}" == "--help" ]]; then
	echo "Usage: $0 [sts2_dll_path] [output_root_dir]"
	echo "Defaults:"
	echo "  sts2_dll_path    $DEFAULT_DLL_PATH"
	echo "  output_root_dir  $DEFAULT_OUTPUT_ROOT"
	exit 0
fi

STS2_DLL_PATH="${1:-$DEFAULT_DLL_PATH}"
OUTPUT_ROOT="${2:-$DEFAULT_OUTPUT_ROOT}"

if [[ ! -f "$STS2_DLL_PATH" ]]; then
	echo "Error: STS2 DLL not found at: $STS2_DLL_PATH"
	exit 1
fi

echo "[1/6] Building decompiler CLI"
dotnet build "$CLI_PROJECT"

NO_LIFT_OUT="$OUTPUT_ROOT/no_lift"
LIFT_OUT="$OUTPUT_ROOT/lift"
NO_LIFT_PROJECT="SlayTheSpire2.NoLift"
LIFT_PROJECT="SlayTheSpire2.Lift"

echo "[2/6] Decompiling STS2 with lifting disabled"
mkdir -p "$NO_LIFT_OUT"
dotnet run --project "$CLI_PROJECT" -- "$STS2_DLL_PATH" \
	--output-dir "$NO_LIFT_OUT" \
	--project-name "$NO_LIFT_PROJECT" \
	--disable-collection-initializer-lifting

echo "[3/6] Building no-lift decompiled project"
dotnet build "$NO_LIFT_OUT/$NO_LIFT_PROJECT.csproj" /p:WarningLevel=0

echo "[4/6] Decompiling STS2 with lifting enabled"
mkdir -p "$LIFT_OUT"
dotnet run --project "$CLI_PROJECT" -- "$STS2_DLL_PATH" \
	--output-dir "$LIFT_OUT" \
	--project-name "$LIFT_PROJECT" \
	--enable-collection-initializer-lifting

echo "[5/6] Building lift-enabled decompiled project"
dotnet build "$LIFT_OUT/$LIFT_PROJECT.csproj" /p:WarningLevel=0

echo "[6/6] Real-world STS2 decompile validation passed"
echo "No-lift output: $NO_LIFT_OUT"
echo "Lift output:    $LIFT_OUT"
