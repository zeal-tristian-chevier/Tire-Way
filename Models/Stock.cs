#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireWay.Models;
public class Stock
{
    [Key]
    public int StockId { get; set; }
    public List<Tire> StockedTires { get; set; } = new List<Tire>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
