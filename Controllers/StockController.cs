using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TireWay.Models;

namespace TireWay.Controllers;

public class StockController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext db;

    public StockController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet("/stock")]
    public IActionResult Index()
    {
        return View("Index");
    }
    [HttpPost("/stock/new")]
    public IActionResult New(Tire newTire)
    {
        if(!ModelState.IsValid){
            return Index();
        }
        if(db.Tires.Any(t => t.Name == newTire.Name))
        {
            ModelState.AddModelError("Name", "already exist");
        }
        Stock? stock = db.Stock.Include(s => s.StockedTires).FirstOrDefault();
        
        if(stock != null){
            db.Tires.Add(newTire);
            stock.StockedTires.Add(newTire);
            db.SaveChanges();
        }
        return RedirectToAction("Index", "Home");
    }
    

    // [HttpPost("/stock/update")]
    // public IActionResult Update(StockHasTires updatedTire)
    // {
    //     if(!ModelState.IsValid){
    //         return Index();
    //     }
    //     Stock? stock = db.Stock.FirstOrDefault();
    //     Tire? tire = db.Tires.FirstOrDefault(t => t.TireId == updatedTire.TireId);
    //     if(stock != null && tire != null){
    //         stock.StockedTires.Add();
    //         db.SaveChanges();
    //     }
        
    //     // List<Stock> TiresInStock = db.Stock.Include(s => s.StockedTires)
    //     // .ToList();
    //     return RedirectToAction("Index", "Home");
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
