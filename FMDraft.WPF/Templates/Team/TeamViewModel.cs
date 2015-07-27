using FMDraft.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Team
{
    public class TeamViewModel : AbstractViewModel
    {
        public TeamViewModel(GameCore core) : base(core)
        {

        }

        public event Action Changed = delegate { };

        public FMDraft.Library.Entities.Team ToData()
        {
            return new Library.Entities.Team()
            {

            };
        }
    }
}
