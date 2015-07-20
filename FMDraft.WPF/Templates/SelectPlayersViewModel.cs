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
            if (IsLoaded)
            {
                SearchedPlayers = new ObservableCollection<Player>(core.QueryService.GetPlayers());

                ProcessSelections = new RelayCommand(() =>
                {
                    core.GameState.DraftPool.AvailablePlayers.Add(SelectedPlayer);
                    //core.GameState.DraftPool.AvailablePlayers.AddRange(SelectedPlayers);
                }, () =>
                {
                    return true;
                    //return SelectedPlayer != null;
                });
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

        //public ObservableCollection<Player> SelectedPlayers { get; set; }
        public ObservableCollection<Player> SearchedPlayers { get; set; }
    }
}
