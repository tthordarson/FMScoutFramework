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
using System.Windows;

namespace FMDraft.WPF.Templates.LeagueSetup
{
    public class LeagueItemViewModel : AbstractViewModel
    {
        public LeagueItemViewModel(GameCore core) : base(core)
        {
            Cities = new ObservableCollection<City>();
            TeamViewModels = new ObservableCollection<TeamViewModel>();
            GenerateRandomTeamsViewModel = new GenerateRandomTeamsViewModel(core);

            GenerateRandomTeamsTogglePopup = new RelayCommand(() =>
            {
                ToggleRandomTeamsPopup = !ToggleRandomTeamsPopup;

                NotifyPropertyChanged("ToggleRandomTeamsPopup");
                //NotifyPropertyChanged("TeamListViewVisibility");
                //NotifyPropertyChanged("GenerateRandomTeamsButtonVisibility");
            });
        }

        public override void Reload(GameCore core)
        {
            base.Reload(core);

            var nation = core.GameState.PrincipalNation;

            Cities.Clear();
            Cities.AddRange(core.QueryService.GetCities(nation));
        }

        public event Action Changed = delegate { };

        public RelayCommand GenerateRandomTeamsTogglePopup { get; private set; }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("NameOrDefault");
                Changed();
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

        public Visibility TeamListViewVisibility
        {
            get
            {
                if (TeamViewModels.Any())
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }

        public Visibility GenerateRandomTeamsButtonVisibility
        {
            get
            {
                if (TeamListViewVisibility == Visibility.Hidden)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }

        public ObservableCollection<TeamViewModel> TeamViewModels { get; set; }
        public ObservableCollection<City> Cities { get; set; }

        private bool _ToggleRandomTeamsPopup;

        public bool ToggleRandomTeamsPopup
        {
            get { return _ToggleRandomTeamsPopup; }
            set { _ToggleRandomTeamsPopup = value; }
        }

        private GenerateRandomTeamsViewModel _GenerateRandomTeamsViewModel;

        public GenerateRandomTeamsViewModel GenerateRandomTeamsViewModel
        {
            get { return _GenerateRandomTeamsViewModel; }
            set
            {
                _GenerateRandomTeamsViewModel = value;
                NotifyPropertyChanged("GenerateRandomTeamsViewModel");
            }
        }

        public League ToData()
        {
            return new League()
            {
                Name = Name,
                Teams = TeamViewModels.Select(x => x.ToData())
            };
        }
    }
}
