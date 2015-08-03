using FMDraft.Library;
using FMDraft.Library.Entities;
using FMDraft.WPF.Templates;
using FMDraft.WPF.Templates.Drafts;
using FMDraft.WPF.Templates.LeagueSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FMDraft.WPF
{
    public class MainWindowViewModel : AbstractViewModel
    {
        private ConfederationViewModel confederationViewModel;
        private DraftPoolViewModel draftPoolViewModel;
        private LeagueSetupMasterViewModel leagueSetupViewModel;
        private DraftMasterViewModel goToDraftViewModel;

        public MainWindowViewModel() : base(null)
        {
            this.core = new GameCore();

            confederationViewModel = new ConfederationViewModel(core);
            draftPoolViewModel = new DraftPoolViewModel(core);
            leagueSetupViewModel = new LeagueSetupMasterViewModel(core);
            goToDraftViewModel = new DraftMasterViewModel(core);

            confederationViewModel.PrincipalNationChanged += () =>
            {
                NotifyPropertyChanged("CanViewLeagueSetup");
            };

            leagueSetupViewModel.Changed += () =>
            {
                NotifyPropertyChanged("IsDraftReady");
            };

            NewGame = new RelayCommand(() => {
                core.Load();
            });

            QuitProgram = new RelayCommand(() =>
            {
                QuitProgramCallback();
            });

            core.LoadFailedCallback += () =>
            {
                LoadFailedCallback();
                IsGameLoaded = false;
            };

            core.LoadCompleteCallback += () =>
            {
                LoadCompleteCallback();
                IsGameLoaded = true;
            };
        }

        public Action LoadCompleteCallback = delegate { };
        public Action LoadFailedCallback = delegate { };
        public Action QuitProgramCallback = delegate { };

        public RelayCommand NewGame { get; private set; }

        public RelayCommand QuitProgram { get; private set; }

        private bool _IsGameLoaded;

        public bool IsGameLoaded
        {
            get { return _IsGameLoaded; }
            set
            {
                _IsGameLoaded = value;
                NotifyPropertyChanged("IsGameLoaded");
            }
        }

        public bool CanViewLeagueSetup
        {
            get
            {
                return this.core.GameState != null && this.core.GameState.PrincipalNation != null;
            }
        }

        public bool IsDraftReady
        {
            get
            {
                var gameState = this.core.GameState;

                if (gameState == null)
                {
                    return false;
                }

                // Truth condition is that there are leagues and every team in every league has been assigned
                // with draftcards and there is either a human manager or a CPU manager has been assigned

                return gameState.Leagues.Any()
                    && gameState.Leagues.All(league => league.Teams.Any() && league.Teams.All(team => 
                    {
                        return team.DraftCards.Any() && (team.ManagerMode == ManagerMode.Player || team.Manager != null);
                    }));
            }
        }

        private TabItem _SelectedTab;

        public TabItem SelectedTab
        {
            get { return _SelectedTab; }
            set
            {
                _SelectedTab = value;
                NotifyPropertyChanged("SelectedTab");
                NotifyPropertyChanged("CurrentViewModel");
            }
        }

        public AbstractViewModel CurrentViewModel
        {
            get
            {
                switch(SelectedTab.Name)
                {
                    case "ConfederationTab":
                        confederationViewModel.Reload(this.core);
                        return confederationViewModel;
                    case "LeagueSetupTab":
                        leagueSetupViewModel.Reload(this.core);
                        return leagueSetupViewModel;
                    case "DraftPoolTab":
                        draftPoolViewModel.Reload(this.core);
                        return draftPoolViewModel;
                    case "DraftTab":
                        goToDraftViewModel.Reload(this.core);
                        return goToDraftViewModel;
                    default:
                        return null;
                }
            }
        }
    }
}
