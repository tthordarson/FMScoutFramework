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
                    HumanControlled = false,
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
            return new Library.Entities.Team()
            {

            };
        }
    }
}
