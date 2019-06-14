using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/PriceEntries")]
    public class PriceEntriesController : ApiController
    {
        private IDemoUnitOfWork uow = new DemoUnitOfWork(new ApplicationDbContext());


        // GET: api/PriceEntries
        [AllowAnonymous]
        public IEnumerable<PriceEntry> GetPriceEntries()
        {
            return GetAllActualPrices();
        }

        [Route("History")]
        [HttpGet]
        public IEnumerable<PriceEntry> GetHistoryPriceEntries()
        {
            return uow.PriceEntryRepository.GetAll();
        }

        // PUT: api/PriceEntries/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutPriceEntry(IEnumerable<PriceEntry> priceEntries)
        {
            foreach (var entry in priceEntries)
            {
                bool addNew = true;
                PriceEntry oldPrice = GetActivePrice(entry.TicketType, entry.CustomerType);
                if (oldPrice != null)
                {
                    addNew = oldPrice.Price != entry.Price;
                    oldPrice.IsActive = !addNew;
                }

                if (addNew)
                {
                    PriceEntry newEntry = new PriceEntry()
                    {
                        CustomerTypeId = entry.CustomerTypeId,
                        PriceDate = DateTime.Now,
                        IsActive = true,
                        Price = entry.Price,
                        TicketType = entry.TicketType,
                    };
                    uow.PriceEntryRepository.Add(newEntry);
                }
            }

            uow.Complete();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }

        IList<PriceEntry> GetAllActualPrices()
        {
            IList<PriceEntry> prices = new List<PriceEntry>();
            foreach (var customerType in uow.CustomerTypeRepository.GetAll())
            {
                foreach (var ticketType in (TicketType[]) Enum.GetValues(typeof(TicketType)))
                {
                    PriceEntry entry = GetActivePrice(ticketType, customerType);
                    if (entry == null)
                    {
                        entry = new PriceEntry()
                        {
                            Id = -1,
                            CustomerType = customerType,
                            CustomerTypeId = customerType.Id,
                            TicketType = ticketType,
                            PriceDate = DateTime.Now,
                            IsActive = false,
                            Price = 0f
                        };
                    }
                    prices.Add(entry);
                }
            }
            return prices;
        }

        PriceEntry GetActivePrice(TicketType ticketType, CustomerType customerType)
        {
            return uow.PriceEntryRepository.GetAll().LastOrDefault(p => 
                       p.TicketType == ticketType && p.CustomerTypeId == customerType.Id && p.IsActive);
        }
    }
}