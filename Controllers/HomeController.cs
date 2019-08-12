using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LandPerf.Models;

using Microsoft.Extensions.Configuration;


namespace LandPerf.Controllers
{
  public class HomeController : Controller
  {
    public HomeController(IConfiguration Configuration)
    {
      configuration = Configuration; // How do I avoid having to set use this constructor for the Configuration objects
    }

    public IConfiguration configuration { get; }

    public IActionResult Index()
    {
    
      return View();
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
}
