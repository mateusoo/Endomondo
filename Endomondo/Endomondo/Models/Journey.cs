using System;
using System.Collections.Generic;

namespace Endomondo.Models
{
    public class Journey
    {
        public int Id { get; set; }

        public double Duration { get; set; }

        public DateTime DateTime { get; set; }

        public double Distance { get; set; }


        public IList<Location> Locations { get; set; }
    }
}
