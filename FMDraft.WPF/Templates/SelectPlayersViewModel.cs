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

            Reload(core);
        }

        public override void Reload(GameCore core)
        {
            base.Reload(core);

            if (IsLoaded)
            {
                SearchedPlayers = new ObservableCollection<Player>(core.QueryService.GetPlayers());
            }
        }

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
