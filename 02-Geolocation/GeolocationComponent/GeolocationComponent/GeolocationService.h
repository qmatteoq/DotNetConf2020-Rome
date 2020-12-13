#pragma once

#include "GeolocationService.g.h"

using namespace winrt;
using namespace Windows::Foundation;

namespace winrt::GeolocationComponent::implementation
{
    struct GeolocationService : GeolocationServiceT<GeolocationService>
    {
        GeolocationService() = default;

        IAsyncOperation<double> GetLatitude();
        IAsyncOperation<double> GetLongitude();
    };
}

namespace winrt::GeolocationComponent::factory_implementation
{
    struct GeolocationService : GeolocationServiceT<GeolocationService, implementation::GeolocationService>
    {
    };
}
