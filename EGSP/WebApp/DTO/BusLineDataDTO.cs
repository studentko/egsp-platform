﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class BusLineDataDTO
    {
        [MaxLength(10)]
        public string LineNumber { get; set; }

        public virtual IList<int> BusStationIds { get; set; }
    }
}