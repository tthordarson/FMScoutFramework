using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Common.Extensions
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this IList<T> source, IEnumerable<T> items)
        {
            items.ToList().ForEach(item => source.Add(item));
        }

        public static void AddRange<T>(this IList<T> source, IList<T> items)
        {
            items.ToList().ForEach(item => source.Add(item));
        }
    }
}
