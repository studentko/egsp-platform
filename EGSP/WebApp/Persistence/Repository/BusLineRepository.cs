using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class BusLineRepository : Repository<BusLine, int>, IBusLineRepository
    {
        public BusLineRepository(DbContext context) : base(context)
        {
        }
    }
}