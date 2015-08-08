using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.CustomElements
{
    public class UpdatableObservableCollection<T> : ObservableCollection<T>
    {
        public UpdatableObservableCollection(IEnumerable<T> initialData): base(initialData)
        {
            
        }

        public void UpdateCollection()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
