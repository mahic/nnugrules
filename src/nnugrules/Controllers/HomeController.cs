using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using nnugclasslib;

public class HomeController : Controller
{
    [HttpGet("/")]
    public string Index() => "Hello from MVC!" + Lib.Libz();

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