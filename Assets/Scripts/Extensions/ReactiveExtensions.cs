using System.Collections.Generic;
using UniRx;

namespace Extensions
{
    public static class ReactiveExtensions
    {
        public static void AddRangeSilently<T>(this ReactiveCollection<T> collection, IEnumerable<T> items)
        {
            var list = (IList<T>)collection;
            foreach (var item in items)
                list.Add(item);
        }
        
        public static void AddSilently<T>(this ReactiveCollection<T> collection, T item)
        {
            var list = (IList<T>)collection;
            list.Add(item);
        }
    }
}