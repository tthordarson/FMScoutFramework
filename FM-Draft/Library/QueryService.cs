using FMScoutFramework.Core;
using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_Draft.Library
{
    public class QueryService
    {
        private FMCore core;

        public QueryService(FMCore core)
        {
            this.core = core;
        }

        public IEnumerable<Nation> GetNations()
        {
            return core.Nations;
        }
    }
}
