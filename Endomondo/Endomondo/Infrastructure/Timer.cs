using System;
using Xamarin.Forms;

namespace Endomondo.Infrastructure
{
    public class Timer : BindableObject
    {
        private TimeSpan _duration;
        private DateTime _startTime;
        private bool _isEnabled;
        
        public event Action Ticked;

        public TimeSpan Duration
        {
            get => _duration;

            private set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }

        public void Start(int seconds = 1)
        {
            _startTime = DateTime.Now;
            _isEnabled = true;

            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                Duration = DateTime.Now - _startTime;

                if (_isEnabled)
                {
                    Ticked?.Invoke();
                }

                return _isEnabled;
            });
        }

        public void Stop()
        {
            _isEnabled = false;
        }
    }
}
