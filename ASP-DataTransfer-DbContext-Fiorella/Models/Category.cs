using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP_DataTransfer_DbContext_Fiorella.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
