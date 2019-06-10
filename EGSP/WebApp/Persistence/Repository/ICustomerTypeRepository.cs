﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public interface ICustomerTypeRepository : IRepository<CustomerType, int>
    {
    }
}