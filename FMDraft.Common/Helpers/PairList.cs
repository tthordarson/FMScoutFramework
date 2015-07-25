using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Common.Helpers
{
    public class PairList<TType1, TType2>
    {
        private List<PairListItem> list;

        public PairList()
        {
            list = new List<PairListItem>();
        }

        public void Add(TType1 item1, TType2 item2)
        {
            list.Add(new PairListItem(item1, item2)) ;
        }

        public IEnumerable<Tuple<TType1, TType2>> Contents
        {
            get
            {
                return list.Select(x =>
                {
                    return new Tuple<TType1, TType2>(x.item1, x.item2);
                });
            }
        }

        private class PairListItem
        {
            public TType1 item1;
            public TType2 item2;

            public PairListItem(TType1 item1, TType2 item2)
            {
                this.item1 = item1;
                this.item2 = item2;
            }
        }
    }
}
