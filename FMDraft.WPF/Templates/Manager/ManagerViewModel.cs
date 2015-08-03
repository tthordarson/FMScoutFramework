using FMDraft.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Manager
{
    public class ManagerViewModel : AbstractViewModel
    {
        public ManagerViewModel(GameCore core) : base(core)
        {

        }

        public int ID { get; set; }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }
    }
}
