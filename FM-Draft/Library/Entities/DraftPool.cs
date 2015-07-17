using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_Draft.Library.Entities
{
    public class DraftPool
    {
        public IEnumerable<Player> AvailablePlayers { get; set; }

        public DraftPool()
        {
            this.AvailablePlayers = new List<Player>();
        }
    }
}
