using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO
{
    public class TicketPurchaseData
    {
        public string CreditCardNumber { get; set; }
        public string CVC { get; set; }
        public string CardExpDate { get; set; }
        public TicketType TicketType { get; set; }
    }
}