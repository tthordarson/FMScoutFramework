using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Entities
{
    public class Team
    {
        public string Name { get; set; }
        public City City { get; set; }
        public int Reputation { get; set; }
        public IEnumerable<DraftCard> DraftCards { get; set; }

        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }

        public Manager Manager { get; set; }
        public ManagerMode ManagerMode { get; set; }

        public Stadium Stadium { get; set; }

        public int DraftOrder { get; set; }

        public Team()
        {
            this.DraftCards = new List<DraftCard>();
        }
    }

    public enum TeamType
    {
        Senior,
        Reserve,
        Youth
    }
}
