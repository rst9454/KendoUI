using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KendoUIApp1.Extension
{
    public class GroupSelector<TElement>
    {
        public Func<TElement, object> Selector { get; set; }
        public string Field { get; set; }
        public IEnumerable<Aggregator> Aggregates { get; set; }

    }
}