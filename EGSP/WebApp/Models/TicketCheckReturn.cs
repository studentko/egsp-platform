using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TicketCheckReturn
    {
        public bool IsValid { get; set; }
        public Ticket Ticket { get; set; }
        public string Message { get; set; }
    }
}