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

        public override void Reload(GameCore core)
        {
            base.Reload(core);
            LeagueItemViews.Clear();
            LeagueItemViews.AddRange(core.GameState.Leagues.Select(x => x.ToViewModel(core)));
        }

        public event Action Changed = delegate { };

        private void UpdateCore()
        {
            core.GameState.Leagues = LeagueItemViews.Select(x => x.ToData());
            Changed();
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
