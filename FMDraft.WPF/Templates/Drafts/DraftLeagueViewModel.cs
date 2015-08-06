using FMDraft.Library;
using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Drafts
{
    public class DraftLeagueViewModel : AbstractViewModel
    {
        private League league;

        public DraftLeagueViewModel(GameCore core, League league): base(core)
        {
            this.league = league;
            ChildViews.Add(new DraftLotteryViewModel(core, league));
        }

        public string Name
        {
            get
            {
                if (league == null)
                {
                    return string.Empty;
                }

                return league.Name;
            }
        }

        public string NameOrDefault
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "New League";
                }

                return Name;
            }
        }
    }

    public static class DraftLeagueViewModelExtensions
    {
        public static DraftLeagueViewModel ToViewModel(this League league, GameCore core)
        {
            return new DraftLeagueViewModel(core, league);
        }
    }
}
