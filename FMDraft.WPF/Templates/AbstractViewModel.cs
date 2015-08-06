using FMDraft.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates
{
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        public GameCore core;

        public AbstractViewModel(GameCore core)
        {
            if (core != null && core.IsLoaded)
            {
                this.core = core;

                core.LoadCompleteCallback += () =>
                {
                    Reload(core);
                };
            }

            ViewHeading = "Abstract View";
            ChildViews = new ObservableCollection<AbstractViewModel>();
            SelectedChildView = null;
        }

        private AbstractViewModel _SelectedChildView;

        public AbstractViewModel SelectedChildView
        {
            get { return _SelectedChildView; }
            set
            {
                _SelectedChildView = value;
                NotifyPropertyChanged("SelectedChildView");
            }
        }

        public ObservableCollection<AbstractViewModel> ChildViews { get; set; }

        public string ViewHeading { get; set; }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool IsLoaded
        {
            get
            {
                return this.core != null && this.core.IsLoaded;
            }
        }

        public virtual void Reload(GameCore core)
        {
            this.core = core;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
