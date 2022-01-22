using DSU21.Models;
using DSU21.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDbRepository _repo;

        public HomeController(ILogger<HomeController> logger, IDbRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // seed database / fylla databas
            var pirate = _repo.GetPirateById(1);
            //var ship = await _repo.AddShipAsync("Black Pearl");
            var ship = _repo.GetShip(1);
            await Task.Delay(0);
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
