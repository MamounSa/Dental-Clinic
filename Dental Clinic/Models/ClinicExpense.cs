public class ClinicExpense
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
   // public ExpenseCategory Category { get; set; } // فئة المصاريف (مثل الأدوية، الرواتب، الخ)
}
