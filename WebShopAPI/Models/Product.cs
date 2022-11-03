namespace WebShopAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductTitle { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductSize { get; set; }
        

    }
}
