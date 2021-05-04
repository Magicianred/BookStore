using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeViewModelServices _homeViewModelServices;

        public HomeController(ILogger<HomeController> logger, IHomeViewModelServices homeViewModelServices)
        {
            _logger = logger;
            _homeViewModelServices = homeViewModelServices;

        }
        public async Task<IActionResult> Index(HomeIndexViewModel vm, int page = 1)
        {
            return View (await _homeViewModelServices.GetHomeIndexViewModel(vm.CategoryId,vm.AuthorId, page, Constants.ITEMS_PER_PAGE));
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
