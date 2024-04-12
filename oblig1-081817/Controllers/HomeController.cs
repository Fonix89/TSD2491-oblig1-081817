using Microsoft.AspNetCore.Mvc;
using oblig1_081817.Models;
using System.Diagnostics;

namespace oblig1_081817.Controllers
{
    public class HomeController : Controller
    {
        private readonly static MatchingGameModels _matchingGameModels = new MatchingGameModels();


        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            return View(_matchingGameModels);
        }

        [HttpPost]
        public IActionResult ButtonClick(string animal, string description)
        {
            _matchingGameModels.ButtonClick(animal, description);
            return View("Index", _matchingGameModels);
        }
    }
}
