# A Guide to Writing Custom Decryptor scripts

If the game you are attempting to recover assets from uses a custom encryption scheme (i.e. not using the standard AES-256-CFB encryption in Godot), you will need to write a custom decryptor script in order to use GDRE Tools to extract and recover the assets.

## When should you write a custom decryptor?
**In the vast maority of cases, this is not necessary**, and the standard decryption will work with the correct encryption key.

*Most issues with decryption involve not having the correct key*. The game in question may be applying permutations to the key after it has been loaded to further obscure it, so a key provided by a key extraction program won't work.

Only pursue this *after* ensuring you have the correct encryption key, and you have verified that the game is not using the standard Godot encryption scheme. This can be done by reverse-engineering the binary in a decompiler like [Ghidra](https://ghidra-sre.org/).

**Support will not be provided for recovering the correct encryption key**.

# Writing the script:
The encryption script should be written in Godot 4.5+ GDScript; support for other languages is not provided.

Each script must extend the `CustomDecryptor` class, and must implement the `_parse_and_decrypt()` method.

Implementing the `_parse_and_decrypt()` method will require a knowledge of the encryption scheme used by the game, and the file structure of the encrypted files.

For reference, a custom decryption script that implements the standard Godot encryption scheme is provided in the [`gdre_standard_encryption.gd`](./gdre_standard_encryption.gd) file.

## File structure:
In most cases, the file structure will be something like this:
- magic (32-bit int, 4 bytes) (optional, not present if it's a file in a PCK)
- md5 (16 bytes)
- length (64-bit int, 8 bytes)
- iv (16 bytes)
- data (`length` bytes + padding to an alignment, usually 16 bytes; all encryption contexts that we provide require 16 byte alignment)

## Encryption scheme:
In most cases, the encryption scheme will be something like this:
- AES-256-CFB
- Camellia-256-CFB
- Aria-256-CFB

## Basic decryption flow:
1. Read the magic number (if present)
2. Read the md5 hash, length, and iv
3. Read the data (length bytes + padding)
4. Decrypt the data using the encryption context
5. Verify the md5 hash of the decrypted data
6. Return the decrypted data

## Script ingestion:
A path to the custom decryption script to be used for project recovery can be set using either the `--custom-decryption-script` command line argument, or by setting it in the GUI in the "Set Encryption Key" menu option.

## IMPORTANT NOTE:
If you are intending to distribute your decryption script, it is **strongly** recommended to not include any secrets (encryption keys, xor keys, etc.) in the script.
In US jurisdictions, this is illegal as it is a violation of the DMCA, and may be illegal in other jurisdictions as well.

# CLASS REFERENCE:
## CustomDecryptor class
The CustomDecryptor class is the base class for all custom decryptors. A custom decryption script must inherit from this class.

### methods:
#### _parse_and_decrypt(file: FileAccess, key: PackedByteArray, non_pack_file: bool) -> Dictionary:
`_parse_and_decrypt()` is a virtual method that must be implemented by the custom decryptor script that decrypts the file.
*Parameters*:
- `file`: `FileAccess`
  - The `FileAccess` handle to the file to decrypt.
  - NOTE: In most cases, this is a handle that points to an offset in the PCK file,
    and `FileAccess` methods like `get_length()` will return the length of the PCK file, not the length of the data in the packed file.
- `key`: `PackedByteArray`
  - The key to use for decryption.
- `non_pack_file`: `bool`
  - `false` means that the file being loaded is part of a PCK, which means that the file does not have a magic number at the beginning (99% of cases)
  - `true` means that the file being loaded is not part of a PCK, which means that the file will have a magic number at the beginning

*Returns*:
  a `Dictionary` that must contain the following keys:
  - `error`: `Error`
    - `OK` if successful, otherwise an error code
  - `length`: `int`
    - the length of the data in the file in bytes
    - NOTE: This is the length of the decrypted data, not the length of the data in the file padded to 16 bytes alignment.
    - i.e. `data.size()` should be equal to `length`, not `length + padding to 16 byte alignment`
  - `data`: `PackedByteArray`
    - The decrypted data


# Encryption contexts:
For conveniences sake, we have provided additional encryption contexts that can be used to decrypt data.
These behave like the [AESContext class in Godot](https://docs.godotengine.org/en/stable/classes/class_aescontext.html), but with support for additional Ciphers, and CFB mode.
These are:
- `AESContextGDRE`
  - Behaves like the [AESContext class in Godot](https://docs.godotengine.org/en/stable/classes/class_aescontext.html), but with CFB support.
- `CamelliaContext`
  - Implements the Camellia cipher.
- `AriaContext`
  - Implements the Aria cipher.

## methods:
- `start(mode: int, key: PackedByteArray, iv: PackedByteArray = PackedByteArray()) -> Error`:
  - Starts the encryption context.
  - Returns an error code if the context could not be started.
- `update(src: PackedByteArray) -> PackedByteArray`:
  - Updates the encryption context with the data to encrypt/decrypt.
  - Returns the encrypted/decrypted data.
- `get_iv_state() -> PackedByteArray`:
  - Returns the current IV state of the encryption context.
  - NOTE: This is only meaningful when the context is started in CBC or CFB mode.
- `finish()`:
  - Finishes the encryption context.

## constants:
All three support the following modes:
- `MODE_ECB_ENCRYPT`
- `MODE_ECB_DECRYPT`
- `MODE_CBC_ENCRYPT`
- `MODE_CBC_DECRYPT`
- `MODE_CFB_ENCRYPT`
- `MODE_CFB_DECRYPT`

