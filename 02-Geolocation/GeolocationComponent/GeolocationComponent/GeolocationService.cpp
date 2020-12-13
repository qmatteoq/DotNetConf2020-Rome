#include "pch.h"
#include "GeolocationService.h"
#include "GeolocationService.g.cpp"

using namespace winrt;
using namespace Windows::Devices::Geolocation;
using namespace Windows::Foundation;

namespace winrt::GeolocationComponent::implementation
{
	IAsyncOperation<double> GeolocationService::GetLatitude()
	{
		Geolocator geolocator;
		Geoposition position{ co_await geolocator.GetGeopositionAsync() };
		return position.Coordinate().Latitude();
	}

	IAsyncOperation<double> GeolocationService::GetLongitude()
	{
		Geolocator geolocator;
		Geoposition position{ co_await geolocator.GetGeopositionAsync() };
		return position.Coordinate().Longitude();
	}
}
