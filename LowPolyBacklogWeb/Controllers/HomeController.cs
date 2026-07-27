using LowPolyBacklogWeb.Models;
using LowPolyBacklogWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LowPolyBacklogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardApiService _dashboardService;

        public HomeController(IDashboardApiService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var summary = await _dashboardService.GetSummaryAsync();
            return View(summary);
        }

    }
}
