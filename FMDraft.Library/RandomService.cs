using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library
{
    public static class RandomService
    {
        private static int seed = 0;

        private static readonly string[] teamNames = new string[]
        {
            "{0} Dreamers", "{0} FC", "FC {0}", "AFC {0}", "{0} Rebels", "{0} All-Stars", "{0} United",
            "{0} Warriors", "{0} Dictators", "{0} Lions", "{0} Tigers", "{0} Dragons", "{0} Underdogs",
            "{0} Ninjas", "{0} Legends"
        };

        private static readonly string[] colors = new string[]
        {
            "#ffffff", "#000000", "#ff0000", "#00ff00", "#0000ff", "#ffff00", "#00ffff", "#ff00ff"
        };

        public static Team GetTeam(City city, IEnumerable<DraftCard> draftCards)
        {
            return new Team()
            {
                Name = teamNames.GetRandom(city.Name),
                BackgroundColor = colors.GetRandom(),
                ForegroundColor = colors.GetRandom(),
                City = city,
                DraftCards = draftCards
            };
        }

        private static string GetRandom(this string[] pool, params string[] arguments)
        {
            var rnd = new Random(seed);
            var index = rnd.Next(0, pool.Length);
            var randomString = pool[index];

            seed++;

            return string.Format(randomString, arguments);
        }
    }
}
