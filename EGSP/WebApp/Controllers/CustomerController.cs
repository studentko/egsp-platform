using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
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
        
        [Route("Document")]
        public HttpResponseMessage PostDocument()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var customer = GetCustomer();
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + customer.Id + "-" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                    customer.DocumentPath = filePath;
                    customer.ValidationStatus = EValidationStatus.NotValidated;
                    uow.Complete();
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        [Route("Document")]
        [HttpGet]
        public HttpResponseMessage GetDocument()
        {
            var customer = GetCustomer();
            if (customer.DocumentPath != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                var stream = new System.IO.FileStream(customer.DocumentPath, System.IO.FileMode.Open);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("DocumentUrl")]
        public HttpResponseMessage GetDocumentUrl(string id)
        {

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var filePath = HttpContext.Current.Server.MapPath("~/" + id);
            var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            return response;
        }

        [Authorize(Roles = "Controller")]
        [Route("NotValidated")]
        public IEnumerable<Customer> GetNotValidated()
        {
            return uow.CustomerRepository.GetAll().ToList().FindAll(c => c.ValidationStatus == EValidationStatus.NotValidated);
        }

        [Authorize(Roles = "Controller")]
        [HttpPut]
        [Route("MakeValid/{id}")]
        public HttpResponseMessage MakeValid(int id)
        {
            Customer c = uow.CustomerRepository.Get(id);
            c.ValidationStatus = EValidationStatus.Valid;
            c.Approoved = true;
            uow.Complete();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "Controller")]
        [HttpPut]
        [Route("MakeDenied/{id}")]
        public HttpResponseMessage MakeDenied(int id)
        {
            Customer c = uow.CustomerRepository.Get(id);
            c.ValidationStatus = EValidationStatus.Denied;
            uow.Complete();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

    }

    
}
