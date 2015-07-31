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

            GenerateStandardDraftCards = new RelayCommand(() =>
            {
                GenerateRandomDraftCards(NumberOfPlayersPerTeam);
            }, CanGenerateStandardDraftCards);
        }

        public void GenerateRandomDraftCards(int howMany)
        {
            Func<int, int, DraftCardViewModel> generateBasicDraftCard = (pickNumber, weeklySalary) =>
            {
                return new DraftCardViewModel(core)
                {
                    PickNumber = pickNumber,
                    ContractLength = 5,
                    WeeklySalary = weeklySalary
                };
            };

            DraftCards.Add(generateBasicDraftCard(1, 350000));
            DraftCards.Add(generateBasicDraftCard(2, 250000));
            DraftCards.Add(generateBasicDraftCard(3, 200000));
            DraftCards.Add(generateBasicDraftCard(4, 180000));
            DraftCards.Add(generateBasicDraftCard(5, 150000));
            DraftCards.Add(generateBasicDraftCard(6, 120000));
            DraftCards.Add(generateBasicDraftCard(7, 100000));
            DraftCards.Add(generateBasicDraftCard(8, 100000));
            DraftCards.Add(generateBasicDraftCard(9, 100000));
            DraftCards.Add(generateBasicDraftCard(10, 100000));
            DraftCards.Add(generateBasicDraftCard(11, 90000));
            DraftCards.Add(generateBasicDraftCard(12, 80000));
            DraftCards.Add(generateBasicDraftCard(13, 70000));
            DraftCards.Add(generateBasicDraftCard(14, 70000));
            DraftCards.Add(generateBasicDraftCard(15, 60000));
            DraftCards.Add(generateBasicDraftCard(16, 50000));

            for (int i = 17; i <= howMany; i++)
            {
                DraftCards.Add(generateBasicDraftCard(i, 40000));
            }
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

        public IEnumerable<DraftCard> ToData()
        {
            return DraftCards.Select(vm =>
            {
                return new DraftCard()
                {
                    ContractSalary = vm.WeeklySalary,
                    ContractYears = vm.ContractLength,
                    MaxCurrentAbility = vm.MaximumAbility
                };
            });
        }
    }
}
