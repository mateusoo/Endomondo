using System;

namespace Endomondo.Infrastructure
{
    public interface IStepCounter
    {
        event StepCountChangedEventHandler StepCountChanged;

        void Start();

        void Stop();

        bool IsAvailable();
    }

    public delegate void StepCountChangedEventHandler(Object sender, StepCountChangedEventArgs e);

    public class StepCountChangedEventArgs : EventArgs
    {
        public virtual double? Value { get; set; }

    }
}
