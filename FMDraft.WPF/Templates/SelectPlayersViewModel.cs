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
        public SelectPlayersViewModel(GameCore core) : base(core)
        {
            if (IsLoaded)
            {
                SelectedPlayers = new ObservableCollection<Player>();
                SearchedPlayers = new ObservableCollection<Player>(core.QueryService.GetPlayers());

                ProcessSelections = new RelayCommand(() =>
                {
                    core.GameState.DraftPool.AvailablePlayers.AddRange(SelectedPlayers);
                    SelectedPlayers.Clear();
                }, () =>
                {
                    return !SelectedPlayers.Any();
                });
            }
        }

        public RelayCommand ProcessSelections { get; private set; }

        public ObservableCollection<Player> SelectedPlayers { get; set; }
        public ObservableCollection<Player> SearchedPlayers { get; set; }
    }
}
