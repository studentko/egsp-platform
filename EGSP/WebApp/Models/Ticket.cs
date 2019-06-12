using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        
        public DateTime PurchaseTime { get; set; }

        public DateTime? CheckinTime { get; set; }

        public TicketType TicketType { get; set; }

        [NotMapped]
        public string TicketTypeString
        {
            get
            {
                return TicketType.ToString();
            }
        }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }

        public string AnonymousCustomerId { get; set; }
    }

    public enum TicketType
    {
        OneHour,
        Daily,
        Monthly,
        Yearly
    }
}