using FMDraft.Library;
using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Drafts.PlayerDraft
{
    public class PlayerDraftRoundViewModel : AbstractViewModel
    {
        private League league;

        public PlayerDraftRoundViewModel(GameCore core, League league) : base(core)
        {
            this.league = league;
        }

        public int RoundNumber { get; set; }

        public string RoundHeading { get { return string.Format("Round {0}", RoundNumber); } }
    }
}
