using Android.App;
using Android.Content;
using System;
using Endomondo.Infrastructure;
using Xamarin.Forms;

namespace Endomondo.Droid.Services
{
    public class Alarm : IAlarm
    {
        public void SetAlarmForBackgroundServices(int delayTimeInSeconds)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan tsEpoch = DateTime.UtcNow.Subtract(epoch);
            long milliSinceEpoch = (long)tsEpoch.TotalMilliseconds;

            var alarmIntent = new Intent(Forms.Context, typeof(BackgroundReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(Forms.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);
            alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, milliSinceEpoch + delayTimeInSeconds * 1000, pendingIntent);
        }

    }
}