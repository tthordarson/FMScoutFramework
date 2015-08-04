using FMDraft.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Drafts
{
    public class DraftMasterViewModel : AbstractViewModel
    {
        public DraftMasterViewModel(GameCore core) : base(core)
        {

        }

        public override void Reload(GameCore core)
        {
            base.Reload(core);
            if (IsLoaded)
            {
                DraftLeagueItems = new ObservableCollection<DraftLeagueViewModel>(core.GameState.Leagues.Select(x => x.ToViewModel(core)));
            }
        }

        public ObservableCollection<DraftLeagueViewModel> DraftLeagueItems { get; set; }
    }
}
