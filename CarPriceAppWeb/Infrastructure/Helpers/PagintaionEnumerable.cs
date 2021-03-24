using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace CarPriceAppWeb.Infrastructure.Helpers
{
    public class PagintaionEnumerable<T> : IEnumerable<T>
    {
        private  T[] _items;

        private readonly int _totalPages;

        private readonly int _pageSize;

        private int _pageNumber = 1;

        private bool HasBackPage => _pageNumber > 1;

        private bool HasNextPage => _pageNumber <= _totalPages;

        public PagintaionEnumerable(IEnumerable<T> items, int pageSize)
        {
            if (items is null) throw new ArgumentNullException(nameof(items));

            if (pageSize <= 0) throw new ArgumentException("was zero or negative", nameof(pageSize));

            _items = items.ToArray();
            _pageSize = pageSize;

            _totalPages = (int)Math.Ceiling(_items.Length / (decimal)_pageSize);
        }

        public void NextPage()
        {
            _pageNumber++;

            if (!HasNextPage)
            {
                _pageNumber--;
            }
        }

        public void BackPage()
        {
            _pageNumber--;

            if (!HasBackPage)
            {
                _pageNumber = 1;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var fromIndex = (_pageNumber - 1) * _pageSize;

            var toIndex = (_pageNumber * _pageSize) > _items.Length
                        ? _items.Length
                        : _pageNumber * _pageSize;

            for (var index = fromIndex; index < toIndex; index++)
            {
                yield return _items[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
