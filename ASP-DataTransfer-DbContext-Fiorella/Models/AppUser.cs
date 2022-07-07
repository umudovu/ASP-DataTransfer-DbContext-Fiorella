using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ASP_DataTransfer_DbContext_Fiorella.Models
{
    public class AppUser:IdentityUser
    {
        [Required]
        public string  Fullname { get; set; }
    }
}
