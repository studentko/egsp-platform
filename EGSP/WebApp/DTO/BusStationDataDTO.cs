using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO
{
    public class BusStationDataDTO
    {
        public int UpdateVersion { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public IList<BusLine> BusLines { get; set; }
    }
}