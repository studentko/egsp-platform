using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class CustomerTypeRepository : Repository<CustomerType, int>, ICustomerTypeRepository
    {
        public CustomerTypeRepository(DbContext context) : base(context)
        {
        }
    }
}