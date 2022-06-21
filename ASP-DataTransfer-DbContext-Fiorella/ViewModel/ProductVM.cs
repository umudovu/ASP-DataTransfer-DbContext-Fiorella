using ASP_DataTransfer_DbContext_Fiorella.Models;
using System.Collections.Generic;

namespace ASP_DataTransfer_DbContext_Fiorella.ViewModel
{
    public class ProductVM
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
