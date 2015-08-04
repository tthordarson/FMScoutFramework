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

                double additionalPlayerMultiplier = 1.4f;
                
                int numberOfSeniorPlayersNeeded = Convert.ToInt32(draftCards.Count(x => !x.MaxAge.HasValue) * additionalPlayerMultiplier);
                int numberOfYouthPlayersNeeded = Convert.ToInt32(draftCards.Count(x => x.MaxAge.HasValue) * additionalPlayerMultiplier);

                // Assumed max age value for simplicity
                int maxAge = 19;

                // BEGIN REWRITE HERE
                // Update QueryService.GetPlayers to take as argument Number of results. Use that to get all
                // best available players in one call.
                for (int i = 0; i < numberOfSeniorPlayersNeeded; i++)
                {
                    AddBestAvailablePlayerToPool();
                }

                // Repeat above here using as argument that they haven't been added before
                for (int i = 0; i < numberOfYouthPlayersNeeded; i++)
                {
                    AddBestAvailablePlayerToPool(x => x.Age <= maxAge);
                }
                // END REWRITE HERE
            });

            Reload(core);

            PickedPlayerIds = new List<int>();
        }

        private void AddBestAvailablePlayerToPool(Func<Player, bool> extraCriteria = null)
        {
            var bestAvailable = core.QueryService.GetPlayers(passive: false, filter: UnpickedPlayerPredicate);

            if (extraCriteria != null)
            {
                bestAvailable = bestAvailable.Where(extraCriteria);
            }

            var pickedPlayer = bestAvailable.OrderByDescending(x => x.CurrentAbility).FirstOrDefault();

            PickedPlayerIds.Add(pickedPlayer.ID);

            PlayersAddedEvent(pickedPlayer);
        }

        private bool UnpickedPlayerPredicate(Player player)
        {
            return !PickedPlayerIds.Contains(player.ID);
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
