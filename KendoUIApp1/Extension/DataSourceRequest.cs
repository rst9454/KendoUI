﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KendoUIApp1.Extension
{
   
        public class DataSourceRequest
        {
            /// <summary>
            /// Specifies how many items to take.
            /// </summary>
            public int Take { get; set; }

            /// <summary>
            /// Specifies how many items to skip.
            /// </summary>
            public int Skip { get; set; }

            /// <summary>
            /// Specifies the requested sort order.
            /// </summary>
            public IEnumerable<Sort> Sort { get; set; }

            /// <summary>
            /// Specifies the requested grouping.
            /// </summary>
            public IEnumerable<Group> Group { get; set; }

            /// <summary>
            /// Specifies the requested aggregators.
            /// </summary>

            public IEnumerable<Aggregator> Aggregate { get; set; }

            /// <summary>
            /// Specifies the requested filter.
            /// </summary>
            public Filter Filter { get; set; }
        }
    }
