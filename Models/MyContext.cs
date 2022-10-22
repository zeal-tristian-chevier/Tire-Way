#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace TireWay.Models;
// the MyContext class representing a session with our MySQL database, allowing us to query for or save data
public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<Tire> Tires { get; set; } 
    public DbSet<Customer> Customers { get; set; } 
    public DbSet<Invoice> Invoices { get; set; } 
    public DbSet<Order> Orders { get; set; } 
    public DbSet<Stock> Stock { get; set; } 
}