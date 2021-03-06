﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class BusStation
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int UpdateVersion { get; set; }

        public virtual IList<BusLine> BusLines { get; set; }
    }
}