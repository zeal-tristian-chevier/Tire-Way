#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireWay.Models;

[NotMapped]
public class SelectCustomer
{
    [Required(ErrorMessage = "is required.")]
    [MinLength(10, ErrorMessage = "must be a valid phone number")]
    [Display(Name = "Phone #")]
    public string SelectPhone { get; set; } 
    // [Required(ErrorMessage = "is required.")]
    // [MinLength(11, ErrorMessage = "must be a valid VIN")]
    // public string SelectVin { get; set; } 
}
