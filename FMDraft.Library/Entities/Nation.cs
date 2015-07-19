using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Entities
{
    public class Nation
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Nation))
                return false;

            return ((Nation)obj).Name == this.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
