using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Drafts
{
    public class NaiveDraftService: IDraftService
    {
        public Player DraftPlayer(Entities.Team team, IEnumerable<Player> availablePlayers)
        {
            if (availablePlayers == null)
            {
                return null;
            }

            var maxAbility = availablePlayers.Max(x => x.CurrentAbility);

            return availablePlayers.FirstOrDefault(x => x.CurrentAbility == maxAbility);
        }
    }
}
