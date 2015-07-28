using FMDraft.Library;
using FMDraft.WPF.Templates.Drafts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.LeagueSetup
{
    public class GenerateRandomTeamsViewModel : AbstractViewModel
    {
        public GenerateRandomTeamsViewModel(GameCore core) : base(core)
        {
            DraftCards = new ObservableCollection<DraftCardViewModel>();
        }

        public ObservableCollection<DraftCardViewModel> DraftCards { get; set; }
    }
}
