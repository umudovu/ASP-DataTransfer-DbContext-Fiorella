using ASP_DataTransfer_DbContext_Fiorella.Models;
using System.Collections.Generic;

namespace ASP_DataTransfer_DbContext_Fiorella.ViewModel
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public SliderContent SliderContent { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Blog> Blogs { get; set; }


    }
}
