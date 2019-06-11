using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO
{
    public class TicketBuyReturn
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Ticket Ticket { get; set; }
    }
}