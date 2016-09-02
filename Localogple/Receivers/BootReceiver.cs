using Android.Content;
using Android.Runtime;
using Localogple.Services;

namespace Localogple.Receivers
{
    [Register("com.villiard.localogple.receivers.BootReceiver")]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent i)
        {
            var intent = new Intent(context, typeof(LocationService));
            context.StartService(intent);
        }
    }
}