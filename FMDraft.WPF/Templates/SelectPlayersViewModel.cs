using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMDraft.Library;
using FMDraft.Common.Extensions;

namespace FMDraft.WPF.Templates
{
    public class SelectPlayersViewModel : AbstractViewModel
    {
        private Player selectedPlayer;

        public SelectPlayersViewModel(GameCore core) : base(core)
        {
            ProcessSelections = new RelayCommand(() =>
            {
                if (SelectedPlayer != null)
                {
                    PlayersAddedEvent(selectedPlayer);
                    SearchedPlayers.Remove(selectedPlayer);
                }
            }, () =>
            {
                return true;
                //return SelectedPlayer != null;
            });

            AutomateSelections = new RelayCommand(() =>
            {
                var draftCards = core.GameState.Leagues.SelectMany(league => league.Teams.SelectMany(team => team.DraftCards));

                int numberOfSeniorPlayersNeeded = draftCards.Count(x => !x.MaxAge.HasValue);
                int numberOfYouthPlayersNeeded = draftCards.Count(x => x.MaxAge.HasValue);

                // Assumed max age value for simplicity
                int maxAge = 19;

                for (int i = 0; i < numberOfSeniorPlayersNeeded; i++)
                {
                    AddBestAvailablePlayerToPool();
                }

                for (int i = 0; i < numberOfYouthPlayersNeeded; i++)
                {
                    AddBestAvailablePlayerToPool(x => x.Age <= maxAge);
                }
            });

            Reload(core);
        }

        private void AddBestAvailablePlayerToPool(Func<Player, bool> extraCriteria = null)
        {
            var bestAvailable = core.QueryService.GetPlayers(UnpickedPlayerPredicate);

            if (extraCriteria != null)
            {
                bestAvailable = bestAvailable.Where(extraCriteria);
            }

            PlayersAddedEvent(bestAvailable.OrderByDescending(x => x.CurrentAbility).FirstOrDefault());
        }

        private bool UnpickedPlayerPredicate(Player player)
        {
            var poolIds = core.GameState.DraftPool.AvailablePlayers.Select(x => x.ID);
            return !poolIds.Contains(player.ID);
        }

        public override void Reload(GameCore core)
        {
            base.Reload(core);

            if (IsLoaded)
            {
                var players = core.QueryService.GetPlayers(passive: false)
                    .Where(UnpickedPlayerPredicate)
                    .OrderByDescending(x => x.CurrentAbility)
                    .Take(200);

                SearchedPlayers = new ObservableCollection<Player>(players);
            }
        }

        public RelayCommand AutomateSelections { get; private set; }

        public RelayCommand ProcessSelections { get; private set; }

        public Player SelectedPlayer
        {
            get
            {
                return this.selectedPlayer;
            }
            set
            {
                this.selectedPlayer = value;
                NotifyPropertyChanged("SelectedPlayer");
            }
        }

        public event Action<Player> PlayersAddedEvent = delegate { };

        public ObservableCollection<Player> SearchedPlayers { get; set; }
    }
}
