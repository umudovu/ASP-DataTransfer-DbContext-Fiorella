using System;

namespace ASP_DataTransfer_DbContext_Fiorella.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string ImgUrl { get; set; }
    }
}
