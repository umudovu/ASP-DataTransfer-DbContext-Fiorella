using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP_DataTransfer_DbContext_Fiorella.Models
{
    public class AppUser:IdentityUser
    {
        [Required]
        public string  Fullname { get; set; }
        public DateTime CreateTime { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
