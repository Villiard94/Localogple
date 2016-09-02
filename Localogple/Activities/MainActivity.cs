using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Localogple.Services;

namespace Localogple.Activities
{
    [Activity(Label = "Localogple", MainLauncher = true, Icon = "@drawable/icon")]
        [Register("com.villiard.localogple.activities.MainActivity")]
    public class MainActivity : Activity
    {
        int count = 1;
        private static EditText _editText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Start location service.
            var locationServiceIntent = new Intent(this, typeof(LocationService));
            StartService(locationServiceIntent);

            //Listen to location updates.
            var locationUpdateReceiver = new LocationUpdateReceiver();
            var intentFilter = new IntentFilter();
            intentFilter.AddAction(Services.LocationService.Actions.LocationUpdate);
            RegisterReceiver(locationUpdateReceiver, intentFilter);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            _editText = FindViewById<EditText>(Resource.Id.editText_locationLog);
        }

        [BroadcastReceiver]
        private class LocationUpdateReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                if (_editText == null)
                {
                    return;
                }

                var position = intent.GetStringExtra(Services.LocationService.Extras.Position);
                _editText.Text += position + "\n";
            }
        }
    }
}

