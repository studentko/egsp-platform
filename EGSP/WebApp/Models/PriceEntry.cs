using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PriceEntry
    {
        [Key]
        public int Id { get; set; }

        public DateTime PriceDate { get; set; }

        public TicketType TicketType { get; set; }

        [NotMapped]
        public string TicketTypeName
        {
            get
            {
                return TicketType.ToString();
            }
        }

        [ForeignKey("CustomerTypeId")]
        public virtual CustomerType CustomerType { get; set; }

        public int CustomerTypeId { get; set; }

        public bool IsActive { get; set; }

        public float Price { get; set; }
    }
}