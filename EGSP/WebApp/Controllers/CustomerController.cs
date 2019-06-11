using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApp.DTO;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        IDemoUnitOfWork uow = new DemoUnitOfWork(new ApplicationDbContext());

        public Customer Get()
        {
            string email = RequestContext.Principal.Identity.Name;
            Customer customer = uow.CustomerRepository.Find(c => c.Email == email).FirstOrDefault();
            return customer;
            
        }

        public void Put(CustomerUpdateDTO cud)
        {
            Customer c = GetCustomer();
            c.Name = cud.Name;
            c.LastName = cud.LastName;
            c.Address = cud.Address;
            c.Birthday = cud.Birthday;
            uow.Complete();
        }

        [Route("CustomerTypes")]
        [AllowAnonymous]
        public IEnumerable<CustomerType> GetCustomerTypes()
        {
            return uow.CustomerTypeRepository.GetAll();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public static Customer GetCustomer(HttpRequestContext context, IDemoUnitOfWork uow)
        {
            string email = context.Principal.Identity.Name;
            Customer customer = uow.CustomerRepository.Find(c => c.Email == email).FirstOrDefault();
            return customer;
        }

        private Customer GetCustomer()
        {
            return GetCustomer(RequestContext, uow);
        }
    }
}
