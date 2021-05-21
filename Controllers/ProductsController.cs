


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
        List<Products> products = new List<Products>();
        while (await result.ReadAsync())
        {
            products.Add(new Products()
            {
                PName = result.GetValue("PName").ToString(),
                PPrice = Decimal.Parse(result.GetValue("PPrice").ToString()),
                PQauantity = Int16.Parse(result.GetValue("PQauantity").ToString()),
                PDescription = result.GetValue("PDescription").ToString(),
                PId = Int16.Parse(result.GetValue("PId").ToString()),
                PType = result.GetValue("PType").ToString(),
            });
        }
        ViewData["products"] = products;
        await connection.CloseAsync();
        return View();
    }

    public async Task<IActionResult> HandleDelete(string productId)
    {
        try
        {

            await connection.OpenAsync();
            mySqlCommand = new MySqlCommand("DELETE FROM products WHERE PId=" + Int16.Parse(productId) + "", connection);
            var result = await mySqlCommand.ExecuteReaderAsync();
            await connection.CloseAsync();
            ViewData["delete_message"] = "Product Successfully Deleted !";
            return View();
        }
        catch (System.Exception)
        {
            ViewData["delete_message"] = "Product Deletion Failed !";
            return View();
        }

    }

    public async Task<IActionResult> Edit(string productId, string action, string editproduct)

    {
        if (action == "E" && productId != "")
        {
            await connection.OpenAsync();
            mySqlCommand = new MySqlCommand("SELECT * from products WHERE PId=" + Int16.Parse(productId) + "", connection);
            var result = await mySqlCommand.ExecuteReaderAsync();
            await result.ReadAsync();
            var product = new Products()
            {
                PName = result.GetValue("PName").ToString(),
                PPrice = Decimal.Parse(result.GetValue("PPrice").ToString()),
                PQauantity = Int16.Parse(result.GetValue("PQauantity").ToString()),
                PDescription = result.GetValue("PDescription").ToString(),
                PId = Int16.Parse(result.GetValue("PId").ToString()),
                PType = result.GetValue("PType").ToString(),
            };
            ViewData["product"] = product;
            await connection.CloseAsync();
        }
        return View();
    }

    public async Task<IActionResult> HandleEdit(string productId, string productname, string productprice, string productqt, string producttype, string description)

    {
        try
        {
            await connection.OpenAsync();
            mySqlCommand = new MySqlCommand("UPDATE products SET PName='" + productname + "',PPrice=" + Decimal.Parse(productprice) + ",PQauantity=" + Int16.Parse(productqt) + ",PDescription='" + description + "',PType='" + producttype + "' WHERE PId=" + Int16.Parse(productId) + "", connection);
            var result = await mySqlCommand.ExecuteReaderAsync();
            await connection.CloseAsync();
            ViewData["update_message"] = "Product Successfully Updated";
            return View();
        }
        catch (System.Exception)
        {

            ViewData["update_message"] = "Product Update Failed !";
            return View();
        }


    }

}
