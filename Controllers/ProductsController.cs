


using System;
using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{

    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Index(string productname,string productprice, string productqt,string producttype)
    {
        
        return View();
    }

    public IActionResult List()
    {
        return View();
    }

}
