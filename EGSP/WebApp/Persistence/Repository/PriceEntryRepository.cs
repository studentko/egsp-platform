using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class PriceEntryRepository : Repository<PriceEntry, int>, IPriceEntryRepository
    {
        public PriceEntryRepository(DbContext context) : base(context)
        {
        }
    }
}