using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [ForeignKey("CustomerType")]
        public int CustomerTypeId { get; set; }

        public CustomerType CustomerType { get; set; }

        [Required]
        public bool Approoved { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [Required]
        public string Email { get; set; }

    }
}