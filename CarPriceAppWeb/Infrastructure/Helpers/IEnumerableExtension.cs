using System.Collections.Generic;

namespace CarPriceAppWeb.Infrastructure.Helpers
{
    public static class IEnumerableExtension
    {
        public static PagintaionEnumerable<T> ToPaginationEnumerable<T>(this IEnumerable<T> items, int pageSize) 
            => new(items, pageSize);
    }
}
