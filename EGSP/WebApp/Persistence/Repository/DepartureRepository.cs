using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class DepartureRepository : Repository<DepartureTable, int>, IDepratureRepository
    {
        public DepartureRepository(DbContext context) : base(context)
        {
        }
    }
}