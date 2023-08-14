
namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public int ID { get; set; }

        public string? UserID { get; set; }
        public string? CouponCode { get; set; }

        public double Discount { get; set; }
        public double CartTotal { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
