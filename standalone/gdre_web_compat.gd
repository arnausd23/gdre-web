## gdre_web_compat.gd
## Autoload that provides web-browser-compatible replacements for:
##   - "Open Folder" → zip the output dir and trigger browser download
##   - Output directory defaulting to user://recovered/ on web
##
## Usage: GDREWebCompat.open_or_download_folder(path)
##        GDREWebCompat.get_output_dir(suggested_path) -> String
##        GDREWebCompat.is_web() -> bool

extends Node

const WEB_OUTPUT_DIR := "user://gdre_recovered"

func _ready() -> void:
	if is_web():
		# Ensure the output dir exists
		DirAccess.make_dir_recursive_absolute(WEB_OUTPUT_DIR)

## Returns true when running inside a browser (Godot Web export).
func is_web() -> bool:
	return OS.get_name() == "Web"

## On desktop: opens the folder in the OS file manager.
## On web: zips the folder and triggers a browser download.
func open_or_download_folder(path: String) -> void:
	if not is_web():
		OS.shell_open(GDRECommon.path_to_uri(path))
		return
	_zip_and_download(path)

## Returns a usable output directory path.
## On web: always returns WEB_OUTPUT_DIR (browser has no directory picker).
## On desktop: returns the supplied suggestion unchanged.
func get_web_output_dir() -> String:
	return WEB_OUTPUT_DIR

# ---------------------------------------------------------------------------
# Internal: zip the virtual-FS directory and push it to the browser as a download.
# ---------------------------------------------------------------------------
func _zip_and_download(dir_path: String) -> void:
	var zip_path := "user://gdre_download.zip"
	var zipper := ZIPPacker.new()
	var err := zipper.open(zip_path, ZIPPacker.APPEND_CREATE)
	if err != OK:
		push_error("GDREWebCompat: could not create zip at %s (err %d)" % [zip_path, err])
		return

	_zip_dir_recursive(zipper, dir_path, dir_path)
	zipper.close()

	# Read the zip back into memory
	var data := FileAccess.get_file_as_bytes(zip_path)
	if data.is_empty():
		push_error("GDREWebCompat: zip file is empty after writing")
		return

	# Trigger browser download via JavaScript
	var folder_name := dir_path.get_file()
	if folder_name.is_empty():
		folder_name = "gdre_recovered"
	var filename := folder_name + ".zip"

	JavaScriptBridge.download_buffer(data, filename, "application/zip")

func _zip_dir_recursive(zipper: ZIPPacker, base_path: String, current_path: String) -> void:
	var da := DirAccess.open(current_path)
	if da == null:
		return
	da.list_dir_begin()
	var item := da.get_next()
	while item != "":
		if item == "." or item == "..":
			item = da.get_next()
			continue
		var full_path := current_path.path_join(item)
		var rel_path  := full_path.trim_prefix(base_path).trim_prefix("/")
		if da.current_is_dir():
			_zip_dir_recursive(zipper, base_path, full_path)
		else:
			zipper.start_file(rel_path)
			var bytes := FileAccess.get_file_as_bytes(full_path)
			zipper.write_file(bytes)
			zipper.close_file()
		item = da.get_next()
	da.list_dir_end()
