using FMDraft.Library;
using FMDraft.Library.Entities;
using FMDraft.WPF.Templates.Drafts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _ForegroundColor;

        public string ForegroundColor
        {
            get { return _ForegroundColor; }
            set { _ForegroundColor = value; }
        }

        private string _BackgroundColor;

        public string BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = value; }
        }

        private City _City;

        public City City
        {
            get { return _City; }
            set { _City = value; }
        }

        public ObservableCollection<DraftCardViewModel> DraftCards { get; set; }

        public FMDraft.Library.Entities.Team ToData()
        {
            return new Library.Entities.Team()
            {

            };
        }
    }
}
