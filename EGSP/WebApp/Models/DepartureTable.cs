using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DepartureTable
    {
        public int Id { get; set; }

        public int UpdateVersion { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public string DayOfWeekString {
            get
            {
                return DayOfWeek.ToString();
            }
        }

        [Required]
        public string DepartureTimes { get; set; }

        [ForeignKey("BusLine")]
        public int BusLineId { get; set; }

        public virtual BusLine BusLine { get; set; }
    }
}