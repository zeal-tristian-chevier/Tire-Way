#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireWay.Models;
public class Tire
{
    [Key]
    public int TireId { get; set; }
    [Required(ErrorMessage = "is required.")]
    [Display(Name = "Part #")]
    public string SKU { get; set; }
    [Required(ErrorMessage = "is required.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "is required.")]
    [Display(Name = "Tire Size")]
    [MinLength(9, ErrorMessage = "correct format to (XXX/XXRXX)")]
    public string TireSize { get; set; }
    [Required(ErrorMessage = "is required.")]
    [Display(Name = "Bin Location")]
    public int Location { get; set; }
    [Required(ErrorMessage = "is required.")]
    [Range(1.00, Double.PositiveInfinity, ErrorMessage = "must be at least $1")]
    [Display(Name = "List Price")]
    public double ListPrice { get; set; }
    [Required]
    [Range(1, Double.PositiveInfinity, ErrorMessage = "must be at least 1")]
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
