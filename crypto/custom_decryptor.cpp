#include "custom_decryptor.h"
#include "core/object/class_db.h"
#include "core/variant/variant.h"

void CustomDecryptor::_bind_methods() {
	ClassDB::bind_method(D_METHOD("parse_and_decrypt", "file", "key", "non_pack_file"), &CustomDecryptor::parse_and_decrypt);
	GDVIRTUAL_BIND(_parse_and_decrypt, "file", "key", "non_pack_file");
}

Dictionary CustomDecryptor::parse_and_decrypt(Ref<FileAccess> p_file, const Vector<uint8_t> &p_key, bool p_non_pack_file) {
	Dictionary result;
	GDVIRTUAL_CALL(_parse_and_decrypt, p_file, p_key, p_non_pack_file, result);
	return result;
}
