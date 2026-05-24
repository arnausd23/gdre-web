#!/bin/bash

SCRIPT_PATH=$(dirname "$0")
cd "$SCRIPT_PATH/../../.."

git fetch --all
git checkout master
git pull

# check for the existence of the 'nikitalita' remote
if ! git remote | grep -q "nikitalita"; then
	git remote add nikitalita https://github.com/nikitalita/godot.git
	git fetch nikitalita
fi

BRANCHES_TO_REBASE=(
	gltf-dupe-images
	material-fix-deprecated-param
	fix-dummy-mesh-blend-shape
	fix-pack-error
	convert-3.x-escn
	fix-svg
	fix-compat-array-shapes
	gltf-buffer-view-encode-fix
)

echo "=== Rebase report ==="

for branch in "${BRANCHES_TO_REBASE[@]}"; do
	# ensure branch exists locally (create from remote if needed)
	if ! git rev-parse --verify "$branch" >/dev/null 2>&1; then
		git checkout -b "$branch" "nikitalita/$branch" 2>/dev/null || {
			echo "SKIP: $branch (could not create from remote)"
			continue
		}
	else
		git checkout "$branch" || {
			echo "SKIP: $branch (checkout failed)"
			continue
		}
	fi

	if git rebase master; then
		echo "SUCCESS: $branch"
	else
		git rebase --abort 2>/dev/null
		echo "FAILED: $branch"
	fi
done

git checkout master
echo "=== Done ==="
