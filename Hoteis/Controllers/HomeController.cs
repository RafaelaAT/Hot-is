using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hoteis.Models;
using Hoteis.Services;

namespace Hoteis.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHotelService _hotelService;

    public HomeController(ILogger<HomeController> logger, IHotelService hotelService)
    {
        _logger = logger;
        _hotelService = hotelService;
    }

    public IActionResult Index(string tipo)
    {
        var hoteis = _hotelService.GetHoteisDto();
        ViewData["filter"] = string.IsNullOrEmpty(tipo) ? "all" : tipo;
        return View(hoteis);
    }

    public IActionResult Details(string Nome)
    {
        var hotel = _hotelService.GetDetailedHotel(Nome);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id
            ?? HttpContext.TraceIdentifier });
    }
}
