﻿using FMDraft.Common.Helpers;
using FMDraft.Library;
using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.LeagueSetup.GenerateRandomTeams
{
    public class CitySourcesViewModel : AbstractViewModel
    {
        private int numberOfTeams;

        public CitySourcesViewModel(GameCore core, int numberOfTeams) : base(core)
        {
            this.numberOfTeams = numberOfTeams;

            CityCount = new ObservableCollection<Pair<City, int>>();

            SearchAndAddCity = new RelayCommand(() =>
            {
                var city = core.QueryService.GetCity(SearchCityName, SearchNation);
                //var city = core.QueryService.GetCities().FirstOrDefault(x => !string.IsNullOrEmpty(SearchCityName) && x.Name.Contains(SearchCityName));

                if (city != null)
                {
                    var pair = new Pair<City, int>(city, 1);

                    CityCount.Add(pair);
                    SearchCityName = "";
                    NotifyPropertyChanged("SearchCityName");
                    NotifyPropertyChanged("CityCount");
                    NotifyPropertyChanged("TeamCountValidatorText");
                    NotifyPropertyChanged("TeamCountValidatorTextColor");
                }
            });

            AllNations = new ObservableCollection<Nation>(core.QueryService.GetNations());
        }

        public RelayCommand SearchAndAddCity { get; set; }

        public ObservableCollection<Pair<City, int>> CityCount { get; set; }

        private string _SearchCityName;

        public string SearchCityName
        {
            get { return _SearchCityName; }
            set
            {
                _SearchCityName = value;
                NotifyPropertyChanged("SearchCityName");
            }
        }

        private Nation _SearchNation;

        public Nation SearchNation
        {
            get { return _SearchNation; }
            set
            {
                _SearchNation = value;
                NotifyPropertyChanged("SearchNation");
            }
        }

        public ObservableCollection<Nation> AllNations { get; set; }

        public int CalculateTeamCountDifference()
        {
            int teamCount = CityCount.Sum(x => x.Item2);

            return numberOfTeams - teamCount;
        }

        public string TeamCountValidatorText
        {
            get
            {
                int difference = CalculateTeamCountDifference();

                if (difference > 0)
                {
                    return string.Format("You need to add {0} more teams", difference);
                }
                else if (difference < 0)
                {
                    return string.Format("You have {0} too many teams", Math.Abs(difference));
                }
                else
                {
                    return string.Format("Valid number of teams");
                }
            }
        }

        public string TeamCountValidatorTextColor
        {
            get
            {
                int difference = CalculateTeamCountDifference();

                if (difference == 0)
                {
                    return "#000000";
                }
                else
                {
                    return "#ff0000";
                }
            }
        }
    }
}
