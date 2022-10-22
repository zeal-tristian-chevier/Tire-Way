using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TireWay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace TireWay.Controllers;

public class CustomersController : Controller
{
    private MyContext db;
    public CustomersController(MyContext context)
    {
        db = context;
    }
    [HttpGet("/customers")]
    public IActionResult Index()
    {
        return View("Index");
    }
    [HttpPost("/customers/register")]
    public IActionResult Register(Customer newCustomer)
    {    
        //Check if model state is valid
        if(!ModelState.IsValid){
            return Index();
        }
        //Check to see if Customer already exist in db
        if(db.Customers.Any(Customer => Customer.Phone == newCustomer.Phone)){
                ModelState.AddModelError("Phone", "already exists");
        }
        // Clear Any Previous customers when switching to a new one, no logout
        HttpContext.Session.Clear();
        //Add Customer to db 
        db.Customers.Add(newCustomer);
        db.SaveChanges();

        Invoice newInvoice = new Invoice()
        {
            CustomerId = newCustomer.CustomerId
        };
        db.Invoices.Add(newInvoice);
        db.SaveChanges();
        //Put Customer in session
        HttpContext.Session.SetInt32("CID", newCustomer.CustomerId);
        HttpContext.Session.SetInt32("InvoiceNumber", newInvoice.InvoiceId);

        return RedirectToAction("Index", "Home");
    }
    [HttpPost("/customers/select")]
    public IActionResult Select(SelectCustomer selectCustomer)
    {
        //Check if model state is valid
        if(!ModelState.IsValid){
            return Index();
        }
        // Clear Any Previous customers when switching to a new one, no logout
        HttpContext.Session.Clear();
        //Check if user exist in db
        Customer? dbCustomer = db.Customers.Include(c => c.CustomerInvoices).FirstOrDefault(c => c.Phone == selectCustomer.SelectPhone);
        if(dbCustomer == null){
            ModelState.AddModelError("SelectPhone", "not found");
            return Index();
        }
        //put Customer in session
        HttpContext.Session.SetInt32("CID", dbCustomer.CustomerId);

        return RedirectToAction("Index", "Home");
    }
}