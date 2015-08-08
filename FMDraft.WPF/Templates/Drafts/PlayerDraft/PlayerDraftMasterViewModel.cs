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

namespace FMDraft.WPF.Templates.Drafts.PlayerDraft
{
    public class PlayerDraftMasterViewModel : AbstractViewModel
    {
        private League league;

        public PlayerDraftMasterViewModel(GameCore core, League league) : base(core)
        {
            this.league = league;

            Teams = new ObservableCollection<TeamViewModel>();
            DraftRounds = new ObservableCollection<PlayerDraftRoundViewModel>();
        }

        public override void Reload(GameCore core)
        {
            base.Reload(core);

            if (IsLoaded)
            {
                Teams.Clear();
                Teams.AddRange(league.Teams.Select(x => x.ToViewModel(core)));

                InitializeDraftRounds();
            }
        }

        private void InitializeDraftRounds()
        {
            DraftRounds.Clear();

            int numberOfRounds = league.Teams.Max(x => x.DraftCards.Count());

            for (int i = 1; i <= numberOfRounds; i++)
            {
                var draftRound = new PlayerDraftRoundViewModel(core, league)
                {
                    
                };
                //DraftRounds.Add()
            }
        }

        public string DraftPanel
        {
            get { return "Draft"; }
        }

        public string DraftPanelForegroundColor
        {
            get { return "#ffffff"; }
        }

        public string DraftPanelBackgroundColor
        {
            get { return "#000000"; }
        }

        public ObservableCollection<TeamViewModel> Teams { get; set; }

        public ObservableCollection<PlayerDraftRoundViewModel> DraftRounds { get; set; }

        private TeamViewModel _SelectedTeam;

        public TeamViewModel SelectedTeam
        {
            get { return _SelectedTeam; }
            set
            {
                _SelectedTeam = value;
                NotifyPropertyChanged("SelectedTeam");
            }
        }

        private PlayerDraftRoundViewModel _SelectedDraftRound;

        public PlayerDraftRoundViewModel SelectedDraftRound
        {
            get { return _SelectedDraftRound; }
            set
            {
                _SelectedDraftRound = value;
                NotifyPropertyChanged("SelectedDraftRound");
            }
        }
    }
}
