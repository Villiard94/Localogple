using Android;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;

namespace Localogple.Services
{
    [Service(Name = "com.villiard.localogple.services.LocationService")]
    public class LocationService : Service
    {
        public abstract class Actions
        {
            public const string LocationUpdate = "LocationUpdateAction";
        }

        public abstract class Extras
        {
            public const string Position = "position";
        }

        private const long MinTime = 10*60*1000;
        private const long MinDistance = 0;

        private IBinder _binder;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            LocationServices.Fused

            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            return _binder = new Binder();
        }

            //var intent = new Intent();
            //intent.SetAction(Actions.LocationUpdate);
            //intent.PutExtra(Extras.Position, locationString);

            //SendBroadcast(intent);
    }
}