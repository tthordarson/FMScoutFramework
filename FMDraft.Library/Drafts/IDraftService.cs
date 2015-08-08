using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Drafts
{
    public interface IDraftService
    {
        Player DraftPlayer(Team team, IEnumerable<Player> availablePlayers);
    }
}
