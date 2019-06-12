using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class BusLine
    {
        public int Id { get; set; }

        [MaxLength(10)]
        [Index("IX_Linenumber", IsUnique = true)]
        public string LineNumber { get; set; }

        public IList<BusStation> BusStations { get; set; }


    }
}