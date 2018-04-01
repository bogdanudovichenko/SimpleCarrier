using System.Collections.Generic;

namespace SimpleCarrier.Common.GetItems
{
    public class FilterModel
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public IEnumerable<FilterModel> SubFilters { get; set; }
    }
}
