using FMDraft.Common.Extensions;
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
            DraftLeagueItems = new ObservableCollection<DraftLeagueViewModel>();
        }

        public override void Reload(GameCore core)
        {
            base.Reload(core);
            if (IsLoaded)
            {
                DraftLeagueItems.Clear();
                DraftLeagueItems.AddRange(core.GameState.Leagues.Select(x => x.ToViewModel(core)));
            }
        }

        public ObservableCollection<DraftLeagueViewModel> DraftLeagueItems { get; set; }
    }
}
