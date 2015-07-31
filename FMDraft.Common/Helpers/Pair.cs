using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Common.Helpers
{
    public class Pair<TType1, TType2>
    {
        public TType1 Item1 { get; set; }
        public TType2 Item2 { get; set; }

        public Pair(TType1 item1, TType2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }
    }
}
