using FMDraft.Library;
using FMDraft.Library.Entities;
using FMDraft.WPF.Templates.Drafts;
using FMDraft.WPF.Templates.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Team
{
    public class TeamViewModel : AbstractViewModel
    {
        public TeamViewModel(GameCore core) : base(core)
        {
            var selectedManagers = core.GameState.Leagues.SelectMany(x => x.Teams.Select(y => y.Manager));

            var managerList = core.QueryService.GetManagers(manager => !selectedManagers.Contains(manager));

            SearchedManagers = new ObservableCollection<ManagerViewModel>(managerList.Select(x =>
            {
                return new ManagerViewModel(core)
                {
                    ID = x.ID,
                    Name = x.FullName
                };
            }));

            ToggleEditManager = new RelayCommand(() =>
            {
                ToggleEditManagerPopup = !ToggleEditManagerPopup;
            });
        }

        public event Action Changed = delegate { };

        public RelayCommand ToggleEditManager { get; set; }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
                Changed();
            }
        }

        private string _ForegroundColor;

        public string ForegroundColor
        {
            get { return _ForegroundColor; }
            set
            {
                _ForegroundColor = value;
                NotifyPropertyChanged("ForegroundColor");
                Changed();
            }
        }

        private string _BackgroundColor;

        public string BackgroundColor
        {
            get { return _BackgroundColor; }
            set
            {
                _BackgroundColor = value;
                NotifyPropertyChanged("BackgroundColor");
                Changed();
            }
        }

        private City _City;

        public City City
        {
            get { return _City; }
            set
            {
                _City = value;
                NotifyPropertyChanged("City");
                Changed();
            }
        }

        private ManagerViewModel _Manager;

        public ManagerViewModel Manager
        {
            get { return _Manager; }
            set
            {
                _Manager = value;
                NotifyPropertyChanged("Manager");
                NotifyPropertyChanged("ManagerNameOrDefault");
                Changed();
            }
        }

        private bool _HumanControlled;

        public bool HumanControlled
        {
            get { return _HumanControlled; }
            set
            {
                _HumanControlled = value;
                NotifyPropertyChanged("HumanControlled");

                if (!value)
                {
                    Name = null;
                    NotifyPropertyChanged("Manager");
                }

                NotifyPropertyChanged("ManagerNameOrDefault");
                Changed();
            }
        }

        public string ManagerNameOrDefault
        {
            get
            {
                if (HumanControlled)
                {
                    return "Human";
                }

                return Manager.Name;
            }
        }

        private bool _ToggleEditManagerPopup;

        public bool ToggleEditManagerPopup
        {
            get { return _ToggleEditManagerPopup; }
            set
            {
                _ToggleEditManagerPopup = value;
                NotifyPropertyChanged("ToggleEditManagerPopup");
            }
        }



        public ObservableCollection<ManagerViewModel> SearchedManagers { get; set; }

        public ObservableCollection<DraftCardViewModel> DraftCards { get; set; }

        public FMDraft.Library.Entities.Team ToData()
        {
            var team = new Library.Entities.Team()
            {
                Name = Name,
                BackgroundColor = BackgroundColor,
                ForegroundColor = ForegroundColor,
                City = City,
                DraftCards = DraftCards.Select(x => x.ToData())
            };

            if (HumanControlled)
            {
                team.ManagerMode = ManagerMode.Player;
            }
            else
            {
                team.ManagerMode = ManagerMode.CPU;

                if (Manager != null)
                {
                    team.Manager = new Library.Entities.Manager()
                    {
                        ID = Manager.ID,
                        FullName = Manager.Name
                    };
                }
            }

            return team;
        }
    }

    public static class TeamViewModelExtensions
    {
        public static TeamViewModel ToViewModel(this FMDraft.Library.Entities.Team team, GameCore core)
        {
            var vm = new TeamViewModel(core)
            {
                BackgroundColor = team.BackgroundColor,
                ForegroundColor = team.ForegroundColor,
                City = team.City,
                Name = team.Name,
                Manager = new ManagerViewModel(core)
            };

            if (team.ManagerMode == ManagerMode.CPU && team.Manager != null)
            {
                vm.Manager.Name = team.Manager.FullName;
                vm.Manager.ID = team.Manager.ID;
            }
            
            if (team.ManagerMode == ManagerMode.Player)
            {
                vm.HumanControlled = true;
            }

            return vm;
        }
    }
}
