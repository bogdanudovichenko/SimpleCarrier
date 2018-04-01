using System.Collections.Generic;

namespace SimpleCarrier.Common.GetItems
{
    public class GetItemsModel
    {
        public IEnumerable<FilterModel> Filters { get; set; }
        public SortingModel Sorting { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}