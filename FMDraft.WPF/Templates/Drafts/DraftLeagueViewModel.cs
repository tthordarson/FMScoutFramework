using FMDraft.Library;
using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Drafts
{
    public class DraftLeagueViewModel : AbstractViewModel
    {
        public DraftLeagueViewModel(GameCore core): base(core)
        {

        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("NameOrDefault");
            }
        }

        public string NameOrDefault
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "New League";
                }

                return Name;
            }
        }
    }

    public static class DraftLeagueViewModelExtensions
    {
        public static DraftLeagueViewModel ToViewModel(this League league, GameCore core)
        {
            return new DraftLeagueViewModel(core)
            {
                Name = league.Name
            };
        }
    }
}
