using System;
using System.Globalization;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Util;
using Localogple.Rest;
using Localogple.Rest.Requests;
using ModernHttpClient;
using ILocationListener = Android.Gms.Location.ILocationListener;

namespace Localogple.Services
{
    [Service(Name = "com.villiard.localogple.services.LocationService")]
    public class LocationService : Service, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, ILocationListener
    {
        public abstract class Actions
        {
            public const string LocationUpdate = "LocationUpdateAction";
        }

        public abstract class Extras
        {
            public const string Position = "position";
        }

        private const long LogTime = 10 * 1000; //10 seconds.
        private const long LogDistance = 200; //200 meters

        private GoogleApiClient _googleApiClient;
        private IInTouchMobileClient _itmClient;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //Keep in mind this service's method can be called multiple times, but only one instance of the service can run at a time.
            if (_googleApiClient == null)
            {
                _googleApiClient = new GoogleApiClient.Builder(this)
                    .AddConnectionCallbacks(this)
                    .AddOnConnectionFailedListener(this)
                    .AddApi(LocationServices.API)
                    .Build();

                _googleApiClient.Connect();
            }

            if (_itmClient == null)
            {
                _itmClient = InTouchMobileRestService.GetClient();
            }

            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public void OnConnected(Bundle connectionHint)
        {
            var locationRequest = new LocationRequest()
                .SetPriority(LocationRequest.PriorityHighAccuracy)
                .SetInterval(LogTime) //10 seconds
                .SetSmallestDisplacement(LogDistance); //10 meters

            LocationServices.FusedLocationApi.RequestLocationUpdates(_googleApiClient, locationRequest, this);
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            StopSelf();
        }

        public void OnConnectionSuspended(int cause)
        {
            LocationServices.FusedLocationApi.RemoveLocationUpdates(_googleApiClient, this);
        }

        public async void OnLocationChanged(Location location)
        {
            var locationString = "Longitude: " + location.Longitude + " Latitude: " + location.Latitude;

            var intent = new Intent();
            intent.SetAction(Actions.LocationUpdate);
            intent.PutExtra(Extras.Position, locationString);

            SendBroadcast(intent);

            Log.Info(PackageName, locationString);

            var request = CreateLogRequest(location);
            try
            {
                await _itmClient.LogLocation(request);
            }
            catch (Exception ex)
            {
                Log.Info(PackageName, ex.Message);
            }
        }

        private LogLocationRequest CreateLogRequest(Location location)
        {
            return new LogLocationRequest
            {
                Dates = new[] { DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) },
                Langugage = "en-US",
                Latitudes = new[] { location.Latitude.ToString(CultureInfo.InvariantCulture) },
                Longitudes = new[] { location.Longitude.ToString(CultureInfo.InvariantCulture) },
                UserId = "sxvilliard"
            };
        }

        public override bool OnUnbind(Intent intent)
        {
            LocationServices.FusedLocationApi.RemoveLocationUpdates(_googleApiClient, this);

            _googleApiClient.Disconnect();
            _googleApiClient = null;

            return base.OnUnbind(intent);
        }
    }
}