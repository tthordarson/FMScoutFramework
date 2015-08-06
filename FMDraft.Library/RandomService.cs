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
            "{0} Ninjas", "{0} Legends", "{0} Gunslingers", "{0} Shooters", "{0} CF", "AS {0}", "{0} Braves",
            "{0} Marlins", "{0} Mets", "{0} Phillies", "{0} Nationals", "{0} Cubs", "{0} Reds", "{0} Lightning",
            "{0} Power", "{0} River Lions", "{0} Express", "{0} Storm", "{0} Dream", "{0} Sky", "{0} Sun",
            "{0} Liberty", "{0} Lynx", "{0} Blitz", "{0} Sharks", "{0} Vikings", "{0} Eagles", "{0} Sting",
            "{0} Rams", "{0} Pirates", "{0} Panthers", "{0} Saints", "{0} Titans", "{0} Raiders", "{0} Giants",
            "{0}", "{0} Gladiators", "{0} Fuel", "{0} Royals", "{0} Thunder", "{0} SC"
        };

        public static Team GetTeam(City city, IEnumerable<DraftCard> draftCards)
        {
            return new Team()
            {
                Name = teamNames.GetRandomString(city.Name),
                BackgroundColor = GetRandomColor(),
                ForegroundColor = GetRandomColor(),
                City = city,
                DraftCards = draftCards
            };
        }

        private static string GetRandomColor()
        {
            var random = GetNextRandomInstance();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));

            return color;
        }

        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            if (!list.Any())
            {
                return default(T);
            }

            var rnd = GetNextRandomInstance();
            var index = rnd.Next(0, list.Count());
            var randomItem = list.ElementAt(index);

            return randomItem;
        }

        private static string GetRandomString(this string[] pool, params string[] arguments)
        {
            return string.Format(pool.GetRandom(), arguments);
        }

        private static Random GetNextRandomInstance()
        {
            var random = new Random(seed);
            seed++;

            return random;
        }
    }
}
