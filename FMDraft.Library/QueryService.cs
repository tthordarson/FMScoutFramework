using FMDraft.Library.Entities;
using FMDraft.Library.Tests;
using FMScoutFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library
{
    public class QueryService
    {
        private FMCore core;
        private Configuration configuration;
        private MockService mockService;

        public QueryService(FMCore core)
        {
            this.core = core;
            this.configuration = new Configuration();
            this.mockService = new MockService();
        }

        public IEnumerable<Manager> GetManagers(Func<Manager, bool> filter = null)
        {
            var query = mockService.GetManagers();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public IEnumerable<Nation> GetNations()
        {
            return mockService.GetNations();

            //return core.Nations
            //    .Select(x =>
            //    {
            //        return new Nation()
            //        {
            //            Name = x.Name
            //        };
            //    })
            //    .OrderBy(x => x.Name);
        }

        public IEnumerable<City> GetCities(Nation nation)
        {
            return mockService.GetCities(nation);
        }

        public IEnumerable<Player> GetPlayers(Func<Player, bool> filter = null, Func<Player, bool> orderBy = null)
        {
            var players = core.Players
                .Select(x =>
                {
                    return new Player()
                    {
                        ID = x.ID,
                        Age = x.Age,
                        Club = x.Club.Name,
                        CurrentAbility = x.CA,
                        DateOfBirth = x.DateOfBirth,
                        FullName = string.Format("{0} {1}", x.Firstname, x.Lastname),
                        Position = x.PlayerStats.Position,
                        PotentialAbility = x.PA
                    };
                });

            if (filter != null)
            {
                players = players.Where(filter);
            }

            if (orderBy != null)
            {
                players = players.OrderBy(orderBy);
            }

            return players.Take(configuration.MaxSearchResults);
        }
    }
}
