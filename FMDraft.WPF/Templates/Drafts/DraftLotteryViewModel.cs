using FMDraft.Common.Extensions;
using FMDraft.Library;
using FMDraft.Library.Entities;
using FMDraft.WPF.CustomElements;
using FMDraft.WPF.Templates.Team;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Drafts
{
    public class DraftLotteryViewModel : AbstractViewModel
    {
        private League league;
        private int nextPickNumber;

        public DraftLotteryViewModel(GameCore core, League league) : base(core)
        {
            ViewHeading = "Draft Lottery";
            this.league = league;
            this.nextPickNumber = 1;

            Teams = new SortedObservableCollection<TeamViewModel>(league.Teams.Select(team => team.ToViewModel(core)));

            NextDraw = new RelayCommand(() =>
            {
                var team = Teams.Where(x => x.DraftOrder == 0).GetRandom();

                if (team != null)
                {
                    team.DraftOrder = nextPickNumber;
                    nextPickNumber++;
                    Teams.UpdateCollection();
                }

                if (Teams.All(x => x.DraftOrder > 0))
                {
                    UpdateLeague();
                }
            });
        }

        private void UpdateLeague()
        {
            var teams = Teams.Select(team => team.ToData());

            league.Teams = teams;

            var oldLeague = core.GameState.Leagues.FirstOrDefault(x => x.Name == league.Name);

            var leagues = core.GameState.Leagues.ToList();

            leagues.ReplaceElement(oldLeague, league);

            core.GameState.Leagues = leagues;
        }

        public RelayCommand NextDraw { get; set; }

        public SortedObservableCollection<TeamViewModel> Teams { get; set; }
    }
}
