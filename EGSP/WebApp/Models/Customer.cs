using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        [ForeignKey("CustomerType")]
        public int CustomerTypeId { get; set; }

        public CustomerType CustomerType { get; set; }

        public bool Approoved { get; set; }

    }
}