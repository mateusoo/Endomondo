using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Endomondo.Models
{
    public class Journey
    {
        public int Id { get; set; }

        public long Duration { get; set; }

        public DateTime StartDateTime { get; set; }

        public double Distance { get; set; }

        [NotMapped]
        public TimeSpan DurationTimeSpan
        {
            get => TimeSpan.FromTicks(Duration);
            set => Duration = value.Ticks;
        }


        public IList<Location> Locations { get; set; }


        public Journey(DateTime startDateTime)
        {
            StartDateTime = startDateTime;
            Locations = new List<Location>();
        }
    }
}
