namespace ASP_DataTransfer_DbContext_Fiorella.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
