using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Common.Helpers
{
    public class PairList<TType1, TType2>
    {
        private List<Pair<TType1, TType2>> list;

        public PairList()
        {
            list = new List<Pair<TType1, TType2>>();
        }

        public void Add(TType1 item1, TType2 item2)
        {
            list.Add(new Pair<TType1, TType2>(item1, item2)) ;
        }

        public IEnumerable<Pair<TType1, TType2>> Contents
        {
            get
            {
                return list;
            }
        }
    }
}
