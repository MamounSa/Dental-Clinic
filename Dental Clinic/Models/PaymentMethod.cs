public class PaymentMethod
{
    public int Id { get; set; }
    public string MethodName { get; set; } // مثل "نقدي"، "بطاقة ائتمان"، "تحويل بنكي"

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
