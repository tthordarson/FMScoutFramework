using FMDraft.Library;
using FMDraft.WPF.Templates;
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
        private DraftPoolViewModel draftViewModel;
        private LeagueSetupMasterViewModel leagueSetupViewModel;

        public MainWindowViewModel() : base(null)
        {
            this.core = new GameCore();

            confederationViewModel = new ConfederationViewModel(core);
            draftViewModel = new DraftPoolViewModel(core);
            leagueSetupViewModel = new LeagueSetupMasterViewModel(core);

            confederationViewModel.PrincipalNationChanged += () =>
            {
                NotifyPropertyChanged("CanViewLeagueSetup");
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
                        draftViewModel.Reload(this.core);
                        return draftViewModel;
                    default:
                        return null;
                }
            }
        }
    }
}
