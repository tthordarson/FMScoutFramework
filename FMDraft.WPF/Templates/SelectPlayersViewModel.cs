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

        private List<int> PickedPlayerIds { get; set; }

        public SelectPlayersViewModel(GameCore core) : base(core)
        {
            AutomateSelections = new RelayCommand(() =>
            {
                var draftCards = core.GameState.Leagues.SelectMany(league => league.Teams.SelectMany(team => team.DraftCards));

                double additionalPlayerMultiplier = 1.4f;
                
                int numberOfSeniorPlayersNeeded = Convert.ToInt32(draftCards.Count(x => !x.MaxAge.HasValue) * additionalPlayerMultiplier);
                int numberOfYouthPlayersNeeded = Convert.ToInt32(draftCards.Count(x => x.MaxAge.HasValue) * additionalPlayerMultiplier);

                // Assumed max age value for simplicity
                int maxAge = 19;

                var bestAvailablePlayers = core.QueryService.GetPlayers(passive: false)
                    .OrderByDescending(x => x.CurrentAbility)
                    .Take(numberOfSeniorPlayersNeeded);

                PickedPlayerIds.AddRange(bestAvailablePlayers.Select(x => x.ID));
                PlayersAddedEvent(bestAvailablePlayers);

                var bestAvailableYoungPlayers = core.QueryService.GetPlayers(passive: false, filter: UnpickedPlayerPredicate)
                    .Where(x => x.Age <= maxAge)
                    .OrderByDescending(x => x.CurrentAbility)
                    .Take(numberOfYouthPlayersNeeded);

                PickedPlayerIds.AddRange(bestAvailableYoungPlayers.Select(x => x.ID));
                PlayersAddedEvent(bestAvailableYoungPlayers);
            });

            Reload(core);

            PickedPlayerIds = new List<int>();
        }

        private bool UnpickedPlayerPredicate(Player player)
        {
            return !PickedPlayerIds.Contains(player.ID);
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

        public event Action<IEnumerable<Player>> PlayersAddedEvent = delegate { };

        public ObservableCollection<Player> SearchedPlayers { get; set; }
    }
}
