using Newtonsoft.Json;
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

        public virtual CustomerType CustomerType { get; set; }

        [Required]
        public bool Approoved { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [Required]
        public string Email { get; set; }

        [JsonIgnore]
        public virtual List<Ticket> Tickets { get; set; }

        [JsonIgnore]
        public string DocumentPath { get; set; }

        [NotMapped]
        public string DocumentUrl
        {
            get
            {
                string file = DocumentPath.Substring(DocumentPath.LastIndexOf('\\') + 1);
                return "http://localhost:52295/api/Customer/DocumentUrl?id=" + file;
            }
        }

        public EValidationStatus ValidationStatus { get; set; }

        [NotMapped]
        public string ValidationStatusString
        {
            get
            {
                return ValidationStatus.ToString();
            }
        }
    }

    public enum EValidationStatus
    {
        NotValidated,
        Validating,
        Valid,
        Denied
    }
}