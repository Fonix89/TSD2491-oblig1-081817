using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;

namespace oblig1_081817.Models
{
    public class MatchingGameModels
    {
        public int matchesFound = 0;

        public string GameStatus { get; set; }

        static List <string> randomEmoji = new List<string>()
        {
            "🦾", "🦾",
            "😶‍🌫️", "😶‍🌫️",
            "🤠", "🤠",
            "🥶", "🥶",
            "👺", "👺",
            "👽", "👽",
            "🦴", "🦴",
            "🍔", "🍔",
        };

        static List <string> animalEmoji = new List<string>()
        {
            "🐶", "🐶",
            "🐺", "🐺",
            "🐮", "🐮",
            "🦊", "🦊",
            "🐱", "🐱",
            "🦁", "🦁",
            "🐯", "🐯",
            "🐭", "🐭",
        };

        static List<string> carEmoji = new List<string>()
        {
            "🚗", "🚗",
            "🚓", "🚓",
            "🚕", "🚕",
            "🛺", "🛺",
            "🛻", "🛻",
            "🚌", "🚌",
            "🚐", "🚐",
            "🚒", "🚒",
        };

        static List<string> weatherEmoji = new List<string>()
            {
                "⛅", "⛅",
                "⛈️", "⛈️",
                "🌤️", "🌤️",
                "🌦️", "🌦️",
                "🌨️", "🌨️",
                "🌧️", "🌧️",
                "☁️", "☁️",
                "🌪️", "🌪️",
            };

        static List<string> foodEmoji = new List<string>()
        {
            "🍕", "🍕",
            "🍔", "🍔",
            "🍟", "🍟",
            "🌭", "🌭",
            "🍿", "🍿",
            "🧀", "🧀",
            "🍖", "🍖",
            "🥩", "🥩",
        };

        static Random random = new Random();
        public List<string> shuffledEmoji = pickRandomEmoji();

        static List<string> pickRandomEmoji()
        {
            int randomIndex = random.Next(0, 5);

            switch (randomIndex)
            {
                case 0:
                    return randomEmoji = randomEmoji.OrderBy(items => random.Next()).ToList(); ;
                case 1:
                    return animalEmoji = animalEmoji.OrderBy(items => random.Next()).ToList(); ;
                case 2:
                    return carEmoji = carEmoji.OrderBy(items => random.Next()).ToList(); ;
                case 3:
                    return weatherEmoji = weatherEmoji.OrderBy(items => random.Next()).ToList(); ;
                case 4:
                    return foodEmoji = foodEmoji.OrderBy(items => random.Next()).ToList(); ;
                default:
                    throw new Exception("Invalid random index");
            }
        }
        private void SetUpGame()
        {
            Random random = new Random();
            shuffledEmoji = pickRandomEmoji();
            
            matchesFound = 0;
        }

        string lastAnimalFound = string.Empty;
        string lastDescription = string.Empty;

        public void ButtonClick(string animal, string animalDescription)
        {
            if (matchesFound == 0)
            {
                GameStatus = "Game Running";
            }
            if (lastAnimalFound == string.Empty)
            {
                // First selection of the pari.  Remember it
                lastAnimalFound = animal;
                lastDescription = animalDescription;
            }
            else if ((lastAnimalFound == animal) && (animalDescription != lastDescription))
            {
                // Match found! Reset for the next pair.
                lastAnimalFound = string.Empty;

                shuffledEmoji = shuffledEmoji
                    .Select(a => a.Replace(animal, string.Empty))
                    .ToList();
                matchesFound++;
                if (matchesFound == 8)
                {
                    GameStatus = "Game Complete";
                    SetUpGame();
                }
            }
            else
            {
                lastAnimalFound = string.Empty;
            }
        }
    }
}
