﻿using FMDraft.Common.Extensions;
using FMDraft.Library;
using FMDraft.Library.Entities;
using FMDraft.WPF.Templates.Drafts;
using FMDraft.WPF.Templates.LeagueSetup.GenerateRandomTeams;
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
        public LeagueItemViewModel(GameCore core)
            : base(core)
        {
            Cities = new ObservableCollection<City>();
            TeamViewModels = new ObservableCollection<TeamViewModel>();
            GenerateRandomTeamsViewModel = new GenerateRandomTeamsViewModel(core);
            Reputation = 200;

            GenerateRandomTeamsTogglePopup = new RelayCommand(() =>
            {
                TogglePopup();
            });

            GenerateRandomTeams = new RelayCommand(() =>
            {
                var teams = GenerateRandomTeamsViewModel.GenerateTeams();

                TeamViewModels.Clear();
                TeamViewModels.AddRange(teams.Select(team => team.ToViewModel(core)));
                TeamViewModels.ForEach(vm => vm.Changed += Changed);

                NotifyPropertyChanged("TeamListViewVisibility");
                NotifyPropertyChanged("GenerateRandomTeamsButtonVisibility");
                TogglePopup();
            });

            Reload(core);
        }

        private void TogglePopup()
        {
            ToggleRandomTeamsPopup = !ToggleRandomTeamsPopup;

            NotifyPropertyChanged("ToggleRandomTeamsPopup");
        }

        public override void Reload(GameCore core)
        {
            base.Reload(core);

            if (IsLoaded)
            {
                AllNations = new ObservableCollection<Nation>(core.QueryService.GetNations());
            }

            Cities.Clear();
            Cities.AddRange(core.QueryService.GetCities());
        }

        public event Action Changed = delegate { };

        public RelayCommand GenerateRandomTeamsTogglePopup { get; private set; }

        public RelayCommand GenerateRandomTeams { get; private set; }

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

        private TeamViewModel _SelectedTeamViewModel;

        public TeamViewModel SelectedTeamViewModel
        {
            get { return _SelectedTeamViewModel; }
            set
            {
                _SelectedTeamViewModel = value;
                NotifyPropertyChanged("SelectedTeamViewModel");
            }
        }

        private Nation _PrincipalNation;

        public Nation PrincipalNation
        {
            get { return _PrincipalNation; }
            set
            {
                _PrincipalNation = value;
                NotifyPropertyChanged("PrincipalNation");
            }
        }

        private int _Reputation;

        public int Reputation
        {
            get { return _Reputation; }
            set 
            { 
                _Reputation = value;
                NotifyPropertyChanged("Reputation");
            }
        }
        

        public ObservableCollection<Nation> AllNations { get; set; }

        public League ToData()
        {
            return new League()
            {
                Name = Name,
                Teams = TeamViewModels.Select(x => x.ToData())
            };
        }
    }

    public static class LeagueItemrViewModelExtensions
    {
        public static LeagueItemViewModel ToViewModel(this League league, GameCore core)
        {
            return new LeagueItemViewModel(core)
            {
                Name = league.Name,
                TeamViewModels = new ObservableCollection<TeamViewModel>(league.Teams.Select(x => x.ToViewModel(core)))
            };
        }
    }
}
