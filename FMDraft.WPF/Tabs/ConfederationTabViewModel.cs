using FMDraft.Library;
using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Tabs
{
    public class ConfederationTabViewModel : INotifyPropertyChanged
    {
        private GameCore core;

        public ConfederationTabViewModel(GameCore core)
        {
            if (core.IsLoaded)
            {
                this.core = core;

                AllNations = new ObservableCollection<Nation>(core.QueryService.GetNations());
            }
        }

        private bool IsLoaded
        {
            get
            {
                return this.core != null && this.core.IsLoaded;
            }
        }

        public Nation PrincipalNation
        {
            get
            {
                if (IsLoaded)
                {
                    return this.core.GameState.PrincipalNation;
                }

                return null;
            }
            set
            {
                if (IsLoaded)
                {
                    this.core.GameState.PrincipalNation = value;
                    NotifyPropertyChanged("PrincipalNation");
                }
            }
        }

        public ObservableCollection<Nation> AllNations { get; set; }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
