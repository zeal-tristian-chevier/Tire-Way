using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TireWay.Models;

namespace TireWay.Controllers;

public class InvoicesController : Controller
{
    private MyContext db;
    public InvoicesController(MyContext context)
    {
        db = context;
    }

}

