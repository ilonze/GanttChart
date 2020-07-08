using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 甘特图像集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GanttChartCollection<T> : ICollection<T>
    {
        private List<T> Items;
        internal bool IsChanged { get; set; }
        internal DateTime StartDate { get; set; }
        public GanttChartCollection()
        {
            Items = new List<T>();
        }
        public GanttChartCollection(ICollection<T> items):this()
        {
            Items.AddRange(items);
            IsChanged = true;
        }
        public int Count => Items.Count;

        bool ICollection<T>.IsReadOnly => false;

        public void Add(T item)
        {
            Items.Add(item);
            IsChanged = true;
        }

        public void Clear()
        {
            Items.Clear();
            IsChanged = false;
        }

        public bool Contains(T item) => Items.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        public bool Remove(T item)
        {
            if (Items.Remove(item))
            {
                IsChanged = true;
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        public T this[int index]
        {
            get
            {
                return Items[index];
            }
        }
    }
}
