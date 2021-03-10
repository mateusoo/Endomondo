using System;

namespace Endomondo.Messages
{
    public class LocationMessage
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime WriteTime { get; set; }
    }
}
