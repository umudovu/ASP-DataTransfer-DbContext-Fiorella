using System.Collections.Generic;

namespace ASP_DataTransfer_DbContext_Fiorella.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
