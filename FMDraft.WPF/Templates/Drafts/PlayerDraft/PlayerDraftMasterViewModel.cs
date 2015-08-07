using FMDraft.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Drafts.PlayerDraft
{
    public class PlayerDraftMasterViewModel : AbstractViewModel
    {
        public PlayerDraftMasterViewModel(GameCore core) : base(core)
        {

        }

        public string DraftPanel
        {
            get { return "Draft"; }
        }

        public string DraftPanelForegroundColor
        {
            get { return "#ffffff"; }
        }

        public string DraftPanelBackgroundColor
        {
            get { return "#000000"; }
        }
    }
}
