using Android.Content;

namespace Endomondo.Droid.Services
{
    [BroadcastReceiver]
    public class BackgroundReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var backgroundServiceIntent = new Intent(context, typeof(BackgroundService));
            context.StartService(backgroundServiceIntent);
        }
    }
}