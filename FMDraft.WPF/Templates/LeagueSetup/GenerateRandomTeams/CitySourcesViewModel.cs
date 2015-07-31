using FMDraft.Common.Helpers;
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
                var nation = core.GameState.PrincipalNation;

                var city = core.QueryService.GetCities(nation).FirstOrDefault(x => !string.IsNullOrEmpty(SearchCityName) && x.Name.Contains(SearchCityName));

                if (city != null)
                {
                    var pair = new Pair<City, int>(city, 1);

                    CityCount.Add(pair);
                    SearchCityName = "";
                    NotifyPropertyChanged("SearchCityName");
                    NotifyPropertyChanged("CityCount");
                }
            });
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

        private bool _IncludeForeignCity;

        public bool IncludeForeignCity
        {
            get { return _IncludeForeignCity; }
            set
            {
                _IncludeForeignCity = value;
                NotifyPropertyChanged("IncludeForeignCity");
            }
        }

    }
}
