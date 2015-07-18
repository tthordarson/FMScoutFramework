using FMScoutFramework.Core;
using FMScoutFramework.Core.Entities.InGame;
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

        public QueryService(FMCore core)
        {
            this.core = core;
            this.configuration = new Configuration();
        }

        public IEnumerable<Nation> GetNations()
        {
            return core.Nations;
        }

        public IEnumerable<Player> GetPlayers(Func<Player, bool> filter)
        {
            return core.Players.Where(filter).Take(configuration.MaxSearchResults);
        }
    }
}
