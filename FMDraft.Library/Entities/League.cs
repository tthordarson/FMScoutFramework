using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Entities
{
    public class League
    {
        public string Name { get; set; }
        public IEnumerable<Team> Teams { get; set; }

        public League()
        {
            this.Teams = new List<Team>();
        }
    }
}
