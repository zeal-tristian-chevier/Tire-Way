#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireWay.Models;
public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    [Required(ErrorMessage = "is required.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } 
    [Required(ErrorMessage = "is required.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } 
    [Required(ErrorMessage = "is required.")]
    public string Email { get; set; } 
    [Required(ErrorMessage = "is required.")]
    [MinLength(10, ErrorMessage = "must be a valid phone number")]
    public string Phone { get; set; } 
    [Required(ErrorMessage = "is required.")]
    [MinLength(9, ErrorMessage = "must be a valid Tire Size. xxx/xx/Rxx")]
    [Display(Name = "Tire Size")]

    public string TireSize { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Order> CustomerOrders { get; set; } = new List<Order>();
    public List<Invoice> CustomerInvoices { get; set; } = new List<Invoice>();
}
