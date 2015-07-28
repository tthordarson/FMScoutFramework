using FMDraft.Common.Extensions;
using FMDraft.Library;
using FMDraft.Library.Entities;
using FMDraft.WPF.Templates.Team;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.LeagueSetup
{
    public class LeagueSetupMasterViewModel : AbstractViewModel
    {
        public LeagueSetupMasterViewModel(GameCore core) : base(core)
        {
            LeagueItemViews = new ObservableCollection<LeagueItemViewModel>();

            AddLeague = new RelayCommand(() =>
            {
                var leagueVm = new LeagueItemViewModel(core);
                leagueVm.Changed += UpdateCore;

                LeagueItemViews.Add(leagueVm);
            });
        }

        private IEnumerable<LeagueItemViewModel> ToViewModels(IEnumerable<League> leagues)
        {
            return leagues.Select(league =>
            {
                var teams = league.Teams.Select(team =>
                {
                    return new TeamViewModel(core)
                    {

                    };
                });

                return new LeagueItemViewModel(core)
                {
                    Name = league.Name,
                    TeamViewModels = new ObservableCollection<TeamViewModel>(teams)
                };
            });
        }

        private void UpdateCore()
        {
            core.GameState.Leagues = LeagueItemViews.Select(x => x.ToData());
        }

        public RelayCommand AddLeague { get; private set; }

        public ObservableCollection<LeagueItemViewModel> LeagueItemViews { get; set; }

        private LeagueItemViewModel _SelectedLeagueItemView;

        public LeagueItemViewModel SelectedLeagueItemView
        {
            get { return _SelectedLeagueItemView; }
            set
            {
                _SelectedLeagueItemView = value;
                NotifyPropertyChanged("SelectedLeagueItemView");
                UpdateCore();
            }
        }
    }
}
