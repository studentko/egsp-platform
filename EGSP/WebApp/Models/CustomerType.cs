﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CustomerType
    {
        public int Id { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public bool NeedApproval { get; set; }

        public bool NeedPhotoId { get; set; }

        public string InstructionsToUser { get; set; }
    }
}