using Microsoft.AspNetCore.Mvc;
using TireWay.Models;
using Microsoft.EntityFrameworkCore;

namespace TireWay.Controllers;

public class TiresController : Controller
{
    private MyContext db;
    public TiresController(MyContext context)
    {
        db = context;
    }
    [HttpGet("/tires")]
    public IActionResult Index()
    {
        Stock? currentStock = db.Stock.Include(s => s.StockedTires).FirstOrDefault();
        return View("Index", currentStock);
    }
    [HttpPost("/invoices/add")]
    public IActionResult AddTire(Tire selectedTire)
    {
        if(!ModelState.IsValid){
            return Index();
        }
        Stock? stock = db.Stock.Include(s => s.StockedTires).FirstOrDefault();
        Invoice? invoice = db.Invoices.Include(i => i.InvoiceTires).FirstOrDefault(i => i.InvoiceId == HttpContext.Session.GetInt32("InvoiceNumber"));
        //check if stock has enough tires
        if(invoice != null && stock != null){
            Tire? invoiceTire = invoice.InvoiceTires.FirstOrDefault(t => t.SKU == selectedTire.SKU);
             Tire? stockTire = stock.StockedTires.FirstOrDefault(t => t.SKU == selectedTire.SKU);
             if(selectedTire.Quantity > stockTire.Quantity)
             {
                ModelState.AddModelError("Quantity", "not enough tires in stock!");
                return Index();
             }
            //if the tire exist on the invoice, only update the quantity
            if(invoiceTire != null)

            {
                invoiceTire.Quantity += selectedTire.Quantity;
                db.SaveChanges();

            } 
            //if the tire doesn't exist on the invoice, add it
            else if(invoiceTire == null)
            {
                invoice.InvoiceTires.Add(selectedTire);
                invoice.Total += (selectedTire.Quantity * selectedTire.ListPrice) * 1.08;
                db.SaveChanges();
            }
            //update the new quantity of stock now that invoice contains quantity
            stockTire.Quantity -= selectedTire.Quantity;
            db.SaveChanges();
        }
        return RedirectToAction("Index", "Home");
    }
}