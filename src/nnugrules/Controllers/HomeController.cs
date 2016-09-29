using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet("/")]
    public string Index() => "Hello from MVC!";

    [HttpGet("/500")]
    public ActionResult Error()
    {
        throw new InvalidOperationException("Error!");
    }

    [HttpGet("/404")]
    public ActionResult FourOFour()
    {
        return NotFound();
    }

    [HttpGet("/Culture")]
    public string Culture() => $"Your culture is {CultureInfo.CurrentCulture.Name}";
}