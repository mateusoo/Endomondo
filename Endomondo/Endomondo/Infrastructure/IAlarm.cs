namespace Endomondo.Infrastructure
{
    public interface IAlarm
    {
        void SetAlarmForBackgroundServices(int delayTimeInSeconds);
    }
}
