#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireWay.Models;
public class Invoice
{
    [Key]
    public int InvoiceId { get; set; }
    public double? Total { get; set; } = 0.00;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int CustomerId { get; set; }
    public List<Tire> InvoiceTires { get; set; } = new List<Tire>();

}
