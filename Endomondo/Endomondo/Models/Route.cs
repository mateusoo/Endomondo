using System;
using System.Collections.Generic;

namespace Endomondo.Models
{
    public class Route
    {
        public int Id { get; set; }

        public double Duration { get; set; }

        public DateTime Date { get; set; }


        public IList<Location> Locations { get; set; }
    }
}
