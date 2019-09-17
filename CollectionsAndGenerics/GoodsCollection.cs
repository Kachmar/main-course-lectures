
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndGenerics
{
    class GoodsCollection<TourType> : IEnumerable<TourType>, IEnumerator<TourType>
    {
        private TourType[] items;
        private int current = -1;
        private TourType _current;

        public void Add(TourType element)
        {
            if (items == null)
            {
                items = new TourType[] { element };
                return;

            }
            TourType[] newItems = new TourType[items.Length + 1];
            items.CopyTo(newItems, 0);
            newItems[items.Length] = element;
            items = newItems;
        }

        public void Remove(TourType element)
        {

        }

        IEnumerator<TourType> IEnumerable<TourType>.GetEnumerator()
        {
            current = -1;
            return this;
        }

        public IEnumerator GetEnumerator()
        {
            current = -1;
            return this;
        }

        public bool MoveNext()
        {
            if (items == null)
            {
                return false;
            }
            if (items.Length - 1 > current)
            {
                current++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        TourType IEnumerator<TourType>.Current => _current;

        public object Current
        {
            get { return items[current]; }
        }

        public void Dispose()
        {
            //this.items = null;
        }
    }
}
