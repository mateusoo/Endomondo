using Android.App;
using Android.Content;
using Android.OS;
using System;
using System.Threading.Tasks;
using Endomondo.Messages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Endomondo.Droid.Services
{
    [Service]
    public class BackgroundService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, 
            StartCommandFlags flags, int startId)
        {
            Task.Run(async () =>
            {
                try
                {
                    var locator = new GeolocationRequest(GeolocationAccuracy.Best, 
                        TimeSpan.FromSeconds(10));
                    var position = await Geolocation.GetLocationAsync(locator);

                    var message = new LocationMessage()
                    {
                        Longitude = position.Longitude,
                        Latitude = position.Latitude,
                        WriteTime = DateTime.Now
                    };

                    Device.BeginInvokeOnMainThread(
                        () => MessagingCenter.Send(message, nameof(LocationMessage))
                    );

                }
                catch (Exception e)
                {

                }
                finally
                {
                    StopSelf();
                }
            });


            return StartCommandResult.Sticky;
        }
    }
}