


using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class ProductsController : Controller
{

    MySqlConnection connection = null;
    MySqlCommand mySqlCommand = null;


    public ProductsController(IConfiguration configuration)
    {
        connection = new MySqlConnection(configuration.GetSection("ConnectionStrings").GetSection("Default").Value);
    }
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Index(string productname, string productprice, string productqt, string producttype, string description)
    {
        await connection.OpenAsync();
        mySqlCommand = new MySqlCommand("INSERT INTO products(PName,PPrice,PQauantity,PDescription,PType) VALUES('" + productname + "'," + Decimal.Parse(productprice) + "," + Int16.Parse(productqt) + ",'" + description + "','" + producttype + "')", connection);
        var result = await mySqlCommand.ExecuteReaderAsync();
        await connection.CloseAsync();
        return View();
    }

    public async Task<IActionResult> List()
    {
        await connection.OpenAsync();
        mySqlCommand = new MySqlCommand("SELECT * from products", connection);
        var result = await mySqlCommand.ExecuteReaderAsync();
        List<dynamic> products = new List<dynamic>();
        while (await result.ReadAsync())
        {
            products.Add(result.GetValue("PName"));
        }
        ViewData["products"] = products;
        await connection.CloseAsync();
        return View();
    }

}
