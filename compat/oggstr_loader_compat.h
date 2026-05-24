#pragma once

#include "compat/resource_loader_compat.h"
#include "core/io/resource.h"
#include "core/object/ref_counted.h"
#include "modules/vorbis/audio_stream_ogg_vorbis.h"

class OggStreamConverterCompat : public ResourceCompatConverter {
	GDCLASS(OggStreamConverterCompat, ResourceCompatConverter);

public:
	virtual Ref<Resource> convert(const Ref<MissingResource> &res, ResourceInfo::LoadType p_type, int ver_major, Error *r_error = nullptr) override;
	virtual bool handles_type(const String &p_type, int ver_major) const override;
};
