using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class BusStationRepository : Repository<BusStation, int>, IBusStationRepository
    {
        public BusStationRepository(DbContext context) : base(context)
        {
        }
    }
}