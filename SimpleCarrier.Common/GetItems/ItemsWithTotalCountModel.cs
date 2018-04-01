using System.Collections.Generic;

namespace SimpleCarrier.Common.GetItems
{
    public class ItemsWithTotalCountModel<T>
    {
        IEnumerable<T> Items { get; set; }
        int TotalCount { get; set; }
    }
}