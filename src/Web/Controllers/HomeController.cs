using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoverPolicyService _coverPolicyService;

        public HomeController(ILogger<HomeController> logger, ICoverPolicyService coverPolicyService)
        {
            _logger = logger;
            this._coverPolicyService = coverPolicyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            CoverPolicyViewModel vm = new CoverPolicyViewModel();
            return View(vm);
        }

        [HttpPost]
        [Route("index/calculatepolicy")]
        public async Task<IActionResult> CalculatePolicy(CoverPolicyViewModel vm)
        {
            vm.Cost = await _coverPolicyService.CalculatePolicy(vm);            
            return View("Index", vm);
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