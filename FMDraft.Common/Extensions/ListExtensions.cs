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

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            source.ToList().ForEach(action);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default; // or pass in as a parameter
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }

        public static void ReplaceElement<T>(this IList<T> list, T oldElement, T newElement)
        {
            int index = list.IndexOf(oldElement);

            if (index < 0)
            {
                return;
            }

            list.Insert(index, newElement);
            list.Remove(oldElement);
        }

        public static void ReplaceElement<T>(this IList<T> list, T newElement, Func<T, bool> predicate)
        {
            var oldElement = list.FirstOrDefault(predicate);
            list.ReplaceElement(oldElement, newElement);
        }
    }
}
