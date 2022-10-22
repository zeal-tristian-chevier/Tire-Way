using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TireWay.Models;

namespace TireWay.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext db;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    public IActionResult Index()
    {
        if(!db.Stock.Any()){
            Stock newStock = new Stock();
            db.Stock.Add(newStock);
            db.SaveChanges();
        }
        if(HttpContext.Session.GetInt32("CID").HasValue){
            int? CID = HttpContext.Session.GetInt32("CID");
            
            Customer? customer = db.Customers.FirstOrDefault(customer => customer.CustomerId == CID);
            if(customer != null){
                Invoice? invoice = db.Invoices.OrderByDescending(i => i.CreatedAt).Include(i => i.InvoiceTires).FirstOrDefault(i => i.CustomerId == customer.CustomerId);
                //Set the total for the invoice 
                invoice.Total = 0.00;
                foreach(Tire tire in invoice.InvoiceTires)
                {
                    invoice.Total += (tire.ListPrice * tire.Quantity);
                }
                ViewModel myModel = new ViewModel();
                myModel.currentCustomer = customer;
                myModel.currentInvoice = invoice;
                HttpContext.Session.SetInt32("InvoiceNumber", invoice.InvoiceId);
                return View("Index", myModel);
            }
        }
        return RedirectToAction("Index", "Customers");
    }

    [HttpPost("/command")]
    public IActionResult Command(String cmd)
    {
        Stock? stock = db.Stock.Include(s => s.StockedTires).FirstOrDefault();
        Invoice? invoice = db.Invoices.Include(s => s.InvoiceTires).FirstOrDefault(i => i.InvoiceId == HttpContext.Session.GetInt32("InvoiceNumber"));
        if(cmd.Length == 4 && stock != null && invoice != null){
            if(cmd[0].Equals('L'))
            {
                int lineNumber = int.Parse(cmd[1].ToString());
                Tire selectedTire = invoice.InvoiceTires[lineNumber - 1];
                Tire? stockTire = stock.StockedTires.FirstOrDefault(t => t.SKU == selectedTire.SKU);
            if(cmd[2].Equals('Q') && stockTire != null)
            {
                int newQuantity = int.Parse(cmd[3].ToString());
                int prevQuantity = selectedTire.Quantity;
                //Update new tire quantity
                if(newQuantity <= 0)
                {
                    invoice.InvoiceTires.Remove(selectedTire);
                    selectedTire.Quantity = newQuantity;
                    //Add tires back to stock
                    stockTire.Quantity += prevQuantity;
                    db.SaveChanges();
                }
                else if(newQuantity < selectedTire.Quantity)
                {
                    selectedTire.Quantity = newQuantity;
                    //Add tires back to stock
                    stockTire.Quantity += prevQuantity - newQuantity;
                    db.SaveChanges();
                }
                else if(newQuantity > selectedTire.Quantity)
                {
                    selectedTire.Quantity = newQuantity;
                    //Take tires out of stock
                    stockTire.Quantity -= newQuantity - prevQuantity;
                    db.SaveChanges();
                }
                db.SaveChanges();
            }
            }
        }
        if(cmd.Length == 2 && stock != null && invoice != null)
        {
            switch(cmd)
            {
                case "11":
                    foreach(Tire tire in invoice.InvoiceTires)
                    {
                        Tire? stockTire = stock.StockedTires.FirstOrDefault(t => t.SKU == tire.SKU);
                        if(stockTire != null)
                        {
                            stockTire.Quantity += tire.Quantity;
                        }
                    }
                    invoice.InvoiceTires.Clear();
                    db.SaveChanges();
                break;
            }
        }
        return Index();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
