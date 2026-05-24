#pragma once
#include "core/io/file_access.h"
#include "core/object/gdvirtual.gen.h"
#include "core/object/ref_counted.h"
#include "core/variant/variant.h"

class CustomDecryptor : public RefCounted {
	GDCLASS(CustomDecryptor, RefCounted);

protected:
	static void _bind_methods();

public:
	virtual Dictionary parse_and_decrypt(Ref<FileAccess> p_file, const Vector<uint8_t> &p_key, bool p_non_pack_file);
	virtual ~CustomDecryptor() {}
	// Dictionary result must have the following keys:
	// - error: Error
	// - length: int
	// - data: PackedByteArray
	GDVIRTUAL3R(Dictionary, _parse_and_decrypt, Ref<FileAccess>, const Vector<uint8_t> &, bool);
};
