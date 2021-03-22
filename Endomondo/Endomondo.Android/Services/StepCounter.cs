using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Hardware;
using Endomondo.Infrastructure;

namespace Endomondo.Droid.Services
{
    public class StepCounter : Java.Lang.Object, ISensorEventListener, IStepCounter
    {
        private readonly SensorManager _sensorManager;
        private readonly Sensor _stepCounter;

        public StepCounter()
        {
            _sensorManager = (SensorManager)Application.Context.GetSystemService(Context.SensorService);
            _stepCounter = _sensorManager.GetDefaultSensor(SensorType.StepCounter);
        }

        public event StepCountChangedEventHandler StepCountChanged;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            StepCountChanged(this, new StepCountChangedEventArgs { Value = e.Values[0] });
        }

        public void Start()
        {
            if (_stepCounter != null)
            {
                _sensorManager.RegisterListener(this, _stepCounter, SensorDelay.Normal);
            }
        }

        public void Stop()
        {
            if (_stepCounter != null)
            {
                _sensorManager.UnregisterListener(this, _stepCounter);
            }
        }

        public bool IsAvailable() => _stepCounter != null;
    }
}