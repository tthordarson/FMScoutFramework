using FMDraft.Library;
using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates
{
    public class ConfederationViewModel : AbstractViewModel
    {
        public ConfederationViewModel(GameCore core) : base(core)
        {
            Reload(core);
        }

        public override void Reload(GameCore core)
        {
            base.Reload(core);

            if (IsLoaded)
            {
                AllNations = new ObservableCollection<Nation>(core.QueryService.GetNations());
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
                    PrincipalNationChanged();
                }
            }
        }

        public event Action PrincipalNationChanged = delegate { };

        public ObservableCollection<Nation> AllNations { get; set; }
    }
}
