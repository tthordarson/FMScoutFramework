using FMDraft.Common.Extensions;
using FMDraft.Library;
using FMDraft.Library.Drafts;
using FMDraft.Library.Entities;
using FMDraft.WPF.CustomElements;
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
        private IDraftService draftService;

        public PlayerDraftMasterViewModel(GameCore core, League league, Func<Player, bool> playerFilter = null) : base(core)
        {
            this.league = league;
            this.draftService = new NaiveDraftService();

            Teams = new ObservableCollection<TeamViewModel>();
            DraftRounds = new UpdatableObservableCollection<PlayerDraftRoundViewModel>(new List<PlayerDraftRoundViewModel>());

            IEnumerable<Player> availablePlayers = core.GameState.DraftPool.AvailablePlayers;

            if (playerFilter != null)
            {
                availablePlayers = availablePlayers.Where(playerFilter);
            }

            AvailablePlayers = new ObservableCollection<Player>(availablePlayers);
            ViewHeading = "Player Draft";

            NextPick = new RelayCommand(ProcessNextPick);
            DraftPlayer = new RelayCommand(ProcessDraftPick);
            AutomatePicks = new RelayCommand(() =>
            {
                while (CanDraftPlayer || CanGoToNextPick)
                {
                    if (CanDraftPlayer)
                    {
                        ProcessDraftPick();
                    }
                    if (CanGoToNextPick)
                    {
                        ProcessNextPick();
                    }
                }
            });

            Reload(core);

            ProcessNextPick();
            SelectHumanTeamIfSingle();
        }

        public bool IsDone { get; set; }

        private void SelectHumanTeamIfSingle()
        {
            var humanTeam = Teams.SingleOrDefault(x => x.HumanControlled);

            if (humanTeam != null)
            {
                SelectedTeam = humanTeam;
            }
        }

        private void ProcessDraftPick()
        {
            Player draftedPlayer = null;

            if (CurrentPick.Team.ManagerMode == ManagerMode.CPU)
            {
                draftedPlayer = draftService.DraftPlayer(CurrentPick.Team, AvailablePlayers.Where(player => CurrentPick.DraftCard.CanDraftPlayer(player)));
            }
            else
            {
                draftedPlayer = SelectedPlayer;
            }

            AvailablePlayers.Remove(draftedPlayer);
            CurrentPick.Player = draftedPlayer;
            SelectedDraftRound.DraftPicks.UpdateCollection();
            NotifyPropertyChanged("CanDraftPlayer");
            NotifyPropertyChanged("CanGoToNextPick");
            NotifyPropertyChanged("DraftPanel");

            SelectedPlayer = null;
        }

        private void ProcessNextPick()
        {
            if (CurrentPick == null)
            {
                var firstRound = DraftRounds.First();

                CurrentPick = firstRound.DraftPicks.OrderBy(x => x.PickNumber).First();
            }

            if (CurrentPick.Player != null)
            {
                var nextPick = GetNextPick();

                CurrentPick = nextPick;
            }

            NotifyPropertyChanged("DraftPanel");
            NotifyPropertyChanged("CanDraftPlayer");
            NotifyPropertyChanged("CanGoToNextPick");
        }

        private DraftCardViewModel GetNextPick()
        {
            var currentRoundNumber = CurrentPick.RoundNumber;
            var currentRound = DraftRounds.ElementAtOrDefault(currentRoundNumber - 1);

            var nextPickNumber = CurrentPick.PickNumber + 1;

            if (currentRound.DraftPicks.Count < nextPickNumber)
            {
                currentRound = DraftRounds.ElementAtOrDefault(currentRoundNumber);
                nextPickNumber = 1;
            }

            if (currentRound == null)
            {
                IsDone = true;
                return null;
            }

            return currentRound.DraftPicks.OrderBy(x => x.PickNumber).ElementAtOrDefault(nextPickNumber - 1);
        }

        public RelayCommand DraftPlayer { get; set; }

        public bool CanDraftPlayer
        {
            get
            {
                if (CurrentPick == null)
                {
                    return false;
                }
                else if (CurrentPick.Team.ManagerMode == ManagerMode.CPU && CurrentPick.Player == null)
                {
                    return true;
                }
                else
                {
                    return SelectedPlayer != null && CurrentPick.Player == null && CurrentPick.DraftCard.CanDraftPlayer(SelectedPlayer);
                }
            }
        }

        public RelayCommand NextPick { get; set; }

        public bool CanGoToNextPick
        {
            get
            {
                if (IsDone)
                {
                    return false;
                }

                if (CurrentPick == null)
                {
                    return true;
                }

                return CurrentPick.Player != null;
            }
        }

        public RelayCommand AutomatePicks { get; set; }

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
                var draftRound = new PlayerDraftRoundViewModel(core, league, i);

                DraftRounds.Add(draftRound);
            }
        }

        public string DraftPanel
        {
            get 
            {
                if (CurrentPick == null)
                {
                    return "Draft";
                }
                
                var output = string.Format("Round {0} - Current Draft - {1} - Pick #{2}", CurrentPick.RoundNumber, CurrentPick.Team.Name, CurrentPick.PickNumber);
                
                if (CurrentPick.Player != null)
                {
                    output += string.Format("- {0}", CurrentPick.Player.FullName);
                }

                return output;
            }
        }

        public string DraftPanelForegroundColor
        {
            get
            {
                if (CurrentPick == null)
                {
                    return "#ffffff";
                }

                return CurrentPick.Team.ForegroundColor;
            }
        }

        public string DraftPanelBackgroundColor
        {
            get 
            {
                if (CurrentPick == null)
                {
                    return "#000000";
                }

                return CurrentPick.Team.BackgroundColor;
            }
        }

        public ObservableCollection<TeamViewModel> Teams { get; set; }

        public UpdatableObservableCollection<PlayerDraftRoundViewModel> DraftRounds { get; set; }

        public ObservableCollection<Player> AvailablePlayers { get; set; }

        private Player _SelectedPlayer;

        public Player SelectedPlayer
        {
            get { return _SelectedPlayer; }
            set 
            { 
                _SelectedPlayer = value;
                NotifyPropertyChanged("SelectedPlayer");
                NotifyPropertyChanged("CanGoToNextPick");
                NotifyPropertyChanged("CanDraftPlayer");
            }
        }
        

        private TeamViewModel _SelectedTeam;

        public TeamViewModel SelectedTeam
        {
            get { return _SelectedTeam; }
            set
            {
                _SelectedTeam = value;

                var updatedDraftCards = GetUpdatedDraftCards(value);

                if (updatedDraftCards != null)
                {
                    _SelectedTeam.DraftCards.Clear();
                    _SelectedTeam.DraftCards.AddRange(GetUpdatedDraftCards(value));
                }

                NotifyPropertyChanged("SelectedTeam");
            }
        }

        private IEnumerable<DraftCardViewModel> GetUpdatedDraftCards(TeamViewModel teamVm)
        {
            if (CurrentPick == null)
            {
                return null;
            }

            return DraftRounds.Where(round => round.RoundNumber < CurrentPick.RoundNumber).Select(round => round.DraftPicks.Where(pick => pick.Player != null).SingleOrDefault(x => x.PickNumber == teamVm.DraftOrder));
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

        private DraftCardViewModel _CurrentPick;

        public DraftCardViewModel CurrentPick
        {
            get { return _CurrentPick; }
            set
            {
                _CurrentPick = value;

                if (value != null)
                {
                    SelectedDraftRound = DraftRounds.ElementAt(CurrentPick.RoundNumber - 1);
                    SelectedTeam = Teams.FirstOrDefault(x => x.Name == CurrentPick.Team.Name);
                }
                else
                {
                    NotifyPropertyChanged("CanDraftPlayer");
                    NotifyPropertyChanged("CanGoToNextPick");
                }

                NotifyPropertyChanged("CurrentPick");
                NotifyPropertyChanged("SelectedDraftRound");
                NotifyPropertyChanged("DraftPanel");
                NotifyPropertyChanged("DraftPanelForegroundColor");
                NotifyPropertyChanged("DraftPanelBackgroundColor");
            }
        }
        
    }
}
