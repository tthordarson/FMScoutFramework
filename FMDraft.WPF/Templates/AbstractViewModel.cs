using FMDraft.Library;
using System;
using System.Collections.Generic;
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
            if (core.IsLoaded)
            {
                this.core = core;
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool IsLoaded
        {
            get
            {
                return this.core != null && this.core.IsLoaded;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
