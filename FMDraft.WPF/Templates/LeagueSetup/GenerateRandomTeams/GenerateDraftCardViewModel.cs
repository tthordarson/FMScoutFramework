using FMDraft.Library;
using FMDraft.Library.Entities;
using FMDraft.WPF.Templates.Drafts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.LeagueSetup.GenerateRandomTeams
{
    public class GenerateDraftCardViewModel : AbstractViewModel
    {
        public GenerateDraftCardViewModel(GameCore core): base(core)
        {
            DraftCards = new ObservableCollection<DraftCardViewModel>();

            NumberOfPlayersPerTeam = 20;
            NumberOfYouthPlayersPerTeam = 14;

            GenerateStandardDraftCards = new RelayCommand(() =>
            {
                GenerateRandomDraftCards(NumberOfPlayersPerTeam, NumberOfYouthPlayersPerTeam);
            }, CanGenerateStandardDraftCards);
        }

        public void GenerateRandomDraftCards(int howMany, int howManyYouth)
        {
            DraftCards.Add(GenerateBasicDraftCard(1, 350000));
            DraftCards.Add(GenerateBasicDraftCard(2, 250000));
            DraftCards.Add(GenerateBasicDraftCard(3, 200000));
            DraftCards.Add(GenerateBasicDraftCard(4, 180000));
            DraftCards.Add(GenerateBasicDraftCard(5, 150000));
            DraftCards.Add(GenerateBasicDraftCard(6, 120000));
            DraftCards.Add(GenerateBasicDraftCard(7, 100000));
            DraftCards.Add(GenerateBasicDraftCard(8, 100000));
            DraftCards.Add(GenerateBasicDraftCard(9, 100000));
            DraftCards.Add(GenerateBasicDraftCard(10, 100000));
            DraftCards.Add(GenerateBasicDraftCard(11, 90000));
            DraftCards.Add(GenerateBasicDraftCard(12, 80000));
            DraftCards.Add(GenerateBasicDraftCard(13, 70000));
            DraftCards.Add(GenerateBasicDraftCard(14, 70000));
            DraftCards.Add(GenerateBasicDraftCard(15, 60000));
            DraftCards.Add(GenerateBasicDraftCard(16, 50000));

            for (int i = 17; i <= howMany; i++)
            {
                DraftCards.Add(GenerateBasicDraftCard(i, 40000));
            }

            for (int i = howMany + 1; i <= howManyYouth + howMany; i++)
            {
                DraftCards.Add(GenerateBasicDraftCard(i, 2000, 19));
            }
        }

        private DraftCardViewModel GenerateBasicDraftCard(int pickNumber, int weeklySalary, int? maxAge = null)
        {
            var vm = new DraftCardViewModel(core)
            {
                RoundNumber = pickNumber,
                ContractLength = 5,
                WeeklySalary = weeklySalary
            };

            if (maxAge.HasValue)
            {
                vm.MaximumAge = maxAge.Value;
            }

            return vm;
        }

        public bool CanGenerateStandardDraftCards()
        {
            return !DraftCards.Any();
        }

        public RelayCommand GenerateStandardDraftCards { get; private set; }

        public ObservableCollection<DraftCardViewModel> DraftCards { get; set; }

        private int _NumberOfPlayersPerTeam;

        public int NumberOfPlayersPerTeam
        {
            get { return _NumberOfPlayersPerTeam; }
            set
            {
                _NumberOfPlayersPerTeam = value;
                NotifyPropertyChanged("NumberOfPlayersPerTeam");
            }
        }

        private int _NumberOfYouthPlayersPerTeam;

        public int NumberOfYouthPlayersPerTeam
        {
            get { return _NumberOfYouthPlayersPerTeam; }
            set
            {
                _NumberOfYouthPlayersPerTeam = value;
                NotifyPropertyChanged("NumberOfYouthPlayersPerTeam");
            }
        }

        public IEnumerable<DraftCard> ToData()
        {
            return DraftCards.Select(vm =>
            {
                return new DraftCard()
                {
                    ContractSalary = vm.WeeklySalary,
                    ContractYears = vm.ContractLength,
                    MaxCurrentAbility = vm.MaximumAbility,
                    MaxAge = vm.MaximumAge,
                    Round = vm.RoundNumber
                };
            });
        }
    }
}
