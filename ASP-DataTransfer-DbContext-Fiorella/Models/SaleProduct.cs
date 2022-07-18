namespace ASP_DataTransfer_DbContext_Fiorella.Models
{
    public class SaleProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
        public double Price { get; set; }
        public int ProductCount { get; set; }
    }
}
