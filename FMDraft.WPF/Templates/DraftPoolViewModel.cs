using FMDraft.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace FMDraft.WPF.Templates
{
    public class DraftPoolViewModel : AbstractViewModel
    {
        private bool openAddPlayersDialog;

        public DraftPoolViewModel(GameCore core) : base(core)
        {
        }

        public IList<Player> AvailablePlayers
        {
            get
            {
                if (IsLoaded)
                {
                    return this.core.GameState.DraftPool.AvailablePlayers;
                }

                return null;
            }
            set
            {
                if (IsLoaded)
                {
                    this.core.GameState.DraftPool.AvailablePlayers = value;
                    NotifyPropertyChanged("AvailablePlayers");
                }
            }
        }
    }
}
