using FMDraft.Library;
using FMDraft.Common.Extensions;
using FMDraft.WPF.Templates.Drafts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.LeagueSetup.GenerateRandomTeams
{
    public class GenerateRandomTeamsViewModel : AbstractViewModel
    {
        private GenerateDraftCardViewModel generateDraftCardVm;
        private CitySourcesViewModel citySourcesVm;

        public GenerateRandomTeamsViewModel(GameCore core) : base(core)
        {
            NumberOfTeams = 20;

            generateDraftCardVm = new GenerateDraftCardViewModel(core);
            citySourcesVm = new CitySourcesViewModel(core, NumberOfTeams);

            EditDraftCards = new RelayCommand(() =>
            {
                RightSideViewModel = generateDraftCardVm;
            });

            EditCitySources = new RelayCommand(() =>
            {
                RightSideViewModel = citySourcesVm;
            });
        }

        private int _NumberOfTeams;

        public int NumberOfTeams
        {
            get { return _NumberOfTeams; }
            set
            {
                _NumberOfTeams = value;
                citySourcesVm = new CitySourcesViewModel(core, NumberOfTeams);
                NotifyPropertyChanged("NumberOfTeams");
                NotifyPropertyChanged("RightSideViewModel");
            }
        }

        public RelayCommand EditDraftCards { get; private set; }

        public RelayCommand EditCitySources { get; private set; }

        public IEnumerable<FMDraft.Library.Entities.Team> GenerateTeams()
        {
            var cities = citySourcesVm.CityCount.AsEnumerable();

            var teams = new List<FMDraft.Library.Entities.Team>();

            cities.ForEach((cityCount) =>
            {
                var city = cityCount.Item1;
                var count = cityCount.Item2;

                for (int i = 0; i < count; i++)
                {
                    teams.Add(RandomService.GetTeam(city));
                }
            });

            return teams;
        }

        private AbstractViewModel _RightSideViewModel;

        public AbstractViewModel RightSideViewModel
        {
            get { return _RightSideViewModel; }
            set
            {
                _RightSideViewModel = value;
                NotifyPropertyChanged("RightSideViewModel");
            }
        }
    }
}
