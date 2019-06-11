using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class TicketCheckinReturn
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public TicketCheckinReturn()
        {

        }

        public TicketCheckinReturn(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }
}