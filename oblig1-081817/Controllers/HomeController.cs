using Microsoft.AspNetCore.Mvc;
using oblig1_081817.Models;
using System.Diagnostics;

namespace oblig1_081817.Controllers
{
    public class HomeController : Controller
    {
        private readonly static MatchingGameModels _matchingGameModels = new MatchingGameModels();
        private static List<User> _users = new List<User>();

        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            if(_matchingGameModels.CurrentUser == null)
            {
                _matchingGameModels.SetUpGame();
            }
            return View(_matchingGameModels);
        }

        [HttpPost]
        public IActionResult ButtonClick(string animal, string description)
        {
            _matchingGameModels.ButtonClick(animal, description);

            // Sjekk om spillet er fullført
            if (_matchingGameModels.GameStatus == "Game Complete" && _matchingGameModels.CurrentUser != null)
            {
                // Hvis spillet er fullført, legg til 1 i antall spill spilt for nåværende bruker
                AddGamesPlayed(_matchingGameModels.CurrentUser.Username);
            }

            return View("Index", _matchingGameModels);
        }


        [HttpPost]
        public IActionResult Register(string Username)
        {
            if(!string.IsNullOrWhiteSpace(Username))
            {
                var userExists = _users.Any(u  => u.Username == Username);
                if (!userExists)
                {
                    var user = new User 
                    {
                        Id = _users.Count + 1,
                        Username = Username,
                        GamesPlayed = 0
                    };
                    _users.Add(user);
                    _matchingGameModels.CurrentUser = user;
                }
                else
                {
                    _matchingGameModels.CurrentUser = _users.FirstOrDefault(u => u.Username == Username);
                }
                return RedirectToAction("Index");
            }
            return View("Index", _matchingGameModels);
        }

        private void AddGamesPlayed(string username)
        {
          
            var user = _users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                user.GamesPlayed++;
            }
        }


        public IActionResult Leaderboard()
        {
            var sortedUsers = _users.OrderByDescending(u => u.GamesPlayed).ToList();
            return View(sortedUsers);
        }

       
    }
}
