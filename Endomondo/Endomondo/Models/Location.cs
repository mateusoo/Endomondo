﻿using System;

namespace Endomondo.Models
{
    public class Location
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime WriteTime { get; set; }


        public int JoruneyId { get; set; }

        public Journey Journey { get; set; }
    }
}
