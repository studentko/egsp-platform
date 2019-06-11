using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.DTO;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Ticket")]
    public class TicketController : ApiController
    {

        IDemoUnitOfWork uow = new DemoUnitOfWork(new ApplicationDbContext());

        public IEnumerable<Ticket> Get()
        {
            Customer customer = GetCustomer();
            return customer.Tickets;
        }

        [Route("Buy")]
        public TicketBuyReturn Buy(TicketPurchaseData tpd)
        {
            TicketBuyReturn tbr = new TicketBuyReturn();

            Customer customer = GetCustomer();

            if (tpd.TicketType != TicketType.OneHour &&
                customer.CustomerType.NeedApproval && !customer.Approoved)
            {
                tbr.IsSuccess = false;
                tbr.ErrorMessage = "Your account is not approoved for this type of ticket";
                return tbr;
            }

            if (!ProcessPayment(tpd))
            {
                tbr.IsSuccess = false;
                tbr.ErrorMessage = "No more money on your card";
                return tbr;
            }

            Ticket ticket = new Ticket()
            {
                CustomerId = customer.Id,
                PurchaseTime = DateTime.Now,
                TicketType = tpd.TicketType,
            };

            uow.TicketRepository.Add(ticket);
            uow.Complete();

            tbr.Ticket = ticket;
            tbr.IsSuccess = true;
            tbr.ErrorMessage = null;

            return tbr;
        }

        [Route("Checkin/{id}")]
        [HttpPut]
        public TicketCheckinReturn Checkin(int id)
        {
            Customer customer = GetCustomer();

            Ticket ticket = customer.Tickets.Find(t => t.Id == id);
            if (ticket == null)
            {
                return new TicketCheckinReturn("User doens't have ticket with id: " + id);
            }

            if (ticket.CheckinTime != null)
            {
                return new TicketCheckinReturn("Ticket allready checked");
            }

            ticket.CheckinTime = DateTime.Now;
            uow.Complete();

            return new TicketCheckinReturn() { IsSuccess = true };
        }

        private Customer GetCustomer()
        {
            return CustomerController.GetCustomer(RequestContext, uow);
        }

        private bool ProcessPayment(TicketPurchaseData tpd)
        {
            // I am dummy payment processor, ha-ha
            return true;
        }
        
    }
}
