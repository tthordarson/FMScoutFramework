using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.CustomElements
{
    public class SortedObservableCollection<T> : ObservableCollection<T>
    {
        public SortedObservableCollection(IEnumerable<T> initialData): base(initialData)
        {
            
        }

        public void UpdateCollection()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
