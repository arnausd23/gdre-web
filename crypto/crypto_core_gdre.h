#pragma once

#include "core/typedefs.h"

class CryptoCoreGdre {
public:
	class CamelliaContext {
	private:
		void *ctx = nullptr;

	public:
		CamelliaContext();
		~CamelliaContext();

		Error set_encode_key(const uint8_t *p_key, size_t p_bits);
		Error set_decode_key(const uint8_t *p_key, size_t p_bits);
		Error encrypt_ecb(const uint8_t p_src[16], uint8_t r_dst[16]);
		Error decrypt_ecb(const uint8_t p_src[16], uint8_t r_dst[16]);
		Error encrypt_cbc(size_t p_length, uint8_t r_iv[16], const uint8_t *p_src, uint8_t *r_dst);
		Error decrypt_cbc(size_t p_length, uint8_t r_iv[16], const uint8_t *p_src, uint8_t *r_dst);
		Error encrypt_cfb(size_t p_length, uint8_t p_iv[16], const uint8_t *p_src, uint8_t *r_dst);
		Error decrypt_cfb(size_t p_length, uint8_t p_iv[16], const uint8_t *p_src, uint8_t *r_dst);
	};

	class AriaContext {
	private:
		void *ctx = nullptr;

	public:
		AriaContext();
		~AriaContext();

		Error set_encode_key(const uint8_t *p_key, size_t p_bits);
		Error set_decode_key(const uint8_t *p_key, size_t p_bits);
		Error encrypt_ecb(const uint8_t p_src[16], uint8_t r_dst[16]);
		Error decrypt_ecb(const uint8_t p_src[16], uint8_t r_dst[16]);
		Error encrypt_cbc(size_t p_length, uint8_t r_iv[16], const uint8_t *p_src, uint8_t *r_dst);
		Error decrypt_cbc(size_t p_length, uint8_t r_iv[16], const uint8_t *p_src, uint8_t *r_dst);
		Error encrypt_cfb(size_t p_length, uint8_t p_iv[16], const uint8_t *p_src, uint8_t *r_dst);
		Error decrypt_cfb(size_t p_length, uint8_t p_iv[16], const uint8_t *p_src, uint8_t *r_dst);
	};
};
