using FMDraft.Common.Extensions;
using FMDraft.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace FMDraft.WPF.Templates
{
    public class DraftPoolViewModel : AbstractViewModel
    {
        public DraftPoolViewModel(GameCore core) : base(core)
        {
            Reload();

            AvailablePlayers.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var item in e.NewItems)
                    {
                        this.core.GameState.DraftPool.AvailablePlayers.Add(item as Player);
                    }
                }
            };

            SearchPlayerViewModel = new SelectPlayersViewModel(core);
            SearchPlayerViewModel.PlayersAddedEvent += (player) =>
            {
                AvailablePlayers.Add(player);
            };
        }

        public void Reload()
        {
            if (IsLoaded)
            {
                AvailablePlayers = new ObservableCollection<Player>(this.core.GameState.DraftPool.AvailablePlayers);
            }
            else
            {
                AvailablePlayers = new ObservableCollection<Player>();
            }
        }

        public ObservableCollection<Player> AvailablePlayers { get; set; }

        private SelectPlayersViewModel _SearchPlayerViewModel;

        public SelectPlayersViewModel SearchPlayerViewModel
        {
            get { return this._SearchPlayerViewModel; }
            set
            {
                this._SearchPlayerViewModel = value;
                NotifyPropertyChanged("SearchPlayerViewModel");
            }
        }
    }
}
