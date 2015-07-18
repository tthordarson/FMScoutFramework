using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Entities
{
    public class GameState
    {
        public Nation PrincipalNation { get; set; }
        public IEnumerable<League> Leagues { get; set; }
        public DraftPool DraftPool { get; set; }

        public GameState()
        {
            this.Leagues = new List<League>();
            this.DraftPool = new DraftPool();
        }
    }
}
