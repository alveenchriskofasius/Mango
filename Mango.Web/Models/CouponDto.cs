namespace Mango.Web.Models
{
    public class CouponDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
