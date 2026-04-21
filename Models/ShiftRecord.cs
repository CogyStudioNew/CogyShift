namespace CogyShift.Models;

public class ShiftRecord
{
    public int Id { get; set; }
    public DateTime CalculatedAt { get; set; }
    public decimal Hours { get; set; }
    public decimal Rate { get; set; }
    public decimal Bonus { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal Tax { get; set; }
    public decimal NetSalary { get; set; }
}