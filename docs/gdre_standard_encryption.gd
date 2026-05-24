class_name CustomDecryptorStandard
extends CustomDecryptor

# This script implements the standard decryption scheme used by Godot.


# PLEASE NOTE: If you are intending to distribute your decryption script, it is *strongly* recommended to not include any secrets (encryption keys, xor keys, etc.) in the script.
# In US jurisdictions, this is illegal as it is a violation of the DMCA and may be illegal in other jurisdictions as well.

# _parse_and_decrypt() is a virtual method that must be implemented by the custom decryptor.
# *Parameters*:
# - file: FileAccess
#   - the file to decrypt.
#   - NOTE: In most cases, this is a handle that points to an offset in the PCK file,
#     and FileAccess methods like `get_length()` will return the length of the PCK file, not the length of the data in the packed file.
# - key: PackedByteArray
#   - the key to use for decryption.
# - non_pack_file: bool
#   - false means that the file being loaded is part of a PCK, which means that the file does not have a magic number at the beginning (99% of cases)
#   - true means that the file being loaded is not part of a PCK, which means that the file will have a magic number at the beginning
#
# *Returns*:
#   a Dictionary that must contain the following keys:
#   - error: Error
#     - OK if successful, otherwise an error code
#   - length: int
#     - the length of the data in the file in bytes
#     - NOTE: This is the length of the decrypted data, not the length of the data in the file padded to 16 bytes alignment.
#     - i.e. `data.size()` should be equal to `length`, not `length + padding to 16 byte alignment`
#   - data: PackedByteArray
#     - the decrypted data
func _parse_and_decrypt(file: FileAccess, key: PackedByteArray, non_pack_file: bool) -> Dictionary:
	# file structure is:
	# - magic (32-bit int, 4 bytes) (optional, not present if it's a file in a PCK)
	# - md5 (16 bytes)
	# - length (64-bit int, 8 bytes)
	# - iv (16 bytes)
	# - data (`length` bytes + padding to 16 byte alignment)

	var result: Dictionary = {
		"error": OK,
		"length": 0,
		"data": PackedByteArray(),
	}

	# Godot normally uses 'GDEC' in ASCII as the magic number found in the header of non-packed encrypted files.
	const GODOT_ENCRYPTED_HEADER_MAGIC = 0x43454447 # little-endian

	# if this isn't a file in a PCK, check that the magic number is correct
	if non_pack_file:
		var magic = file.get_32()
		if magic != GODOT_ENCRYPTED_HEADER_MAGIC:
			result["error"] = ERR_INVALID_PARAMETER
			printerr("Invalid magic")
			return result


	# The standard Godot encryption scheme uses AES-256-CFB, so check that the key is 32 bytes long
	if key.size() != 32:
		result["error"] = ERR_INVALID_PARAMETER
		printerr("Invalid key length; key must be 32 bytes long for AES-256-CFB")
		return result

	# get the md5, length, iv
	var md5 = file.get_buffer(16)
	var length = file.get_64()
	var iv = file.get_buffer(16)
	result["length"] = length

	# check if the file is long enough to contain the data of the given length
	var base = file.get_position()
	if file.get_length() < base + length:
		result["error"] = ERR_FILE_CORRUPT
		printerr("File is too short to contain the data")
		return result

	# data has to be padded to 16 bytes, so the length of the data in file is actually a multiple of 16
	var ds = length;
	if ds % 16 != 0:
		ds += 16 - (ds % 16)
	var data = file.get_buffer(ds)
	# check that the data we read matches the expected size
	if (data.size() != ds):
		result["error"] = ERR_FILE_CORRUPT
		printerr("Data size mismatch")
		return result

	# decrypt the data
	# AES in CFB mode is used for the standard encryption scheme
	var ctx = AESContextGDRE.new()
	ctx.start(AESContextGDRE.MODE_CFB_DECRYPT, key, iv)
	var decrypted = ctx.update(data)
	ctx.finish()

	# truncate the data to the original length
	decrypted.resize(length)

	# MD5 hash the decrypted data
	var md5_ctx = HashingContext.new()
	var err = md5_ctx.start(HashingContext.HASH_MD5)
	if err != OK:
		result["error"] = ERR_BUG
		printerr("Failed to start MD5 hashing")
		return result
 	# don't update the hashing context if the data is empty; we get the default MD5 hash of an empty array
	if decrypted.size() > 0:
		err = md5_ctx.update(decrypted)
		if err != OK:
			result["error"] = ERR_BUG
			printerr("Failed to update MD5 hashing")
			return result
	var md5_hash = md5_ctx.finish()

	# Check that the md5 of the decrypted data matches the expected md5
	if md5_hash.hex_encode() != md5.hex_encode():
		result["error"] = ERR_FILE_CORRUPT
		printerr("MD5 mismatch, incorrect decryption key?")
		return result
	result["data"] = decrypted
	return result
