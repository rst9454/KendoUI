﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KendoUIApp1.Extension
{
    [KnownType("GetKnownTypes")]
    public class DataSourceResult
    {
        /// <summary>
        /// Represents a single page of processed data.
        /// </summary>
        public IEnumerable Data { get; set; }

        /// <summary>
        /// Represents a single page of processed grouped data.
        /// </summary>
        public IEnumerable Group { get; set; }

        /// <summary>
        /// The total number of records available.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Represents a requested aggregates.
        /// </summary>
        public object Aggregates { get; set; }

        /// <summary>
        /// Used by the KnownType attribute which is required for WCF serialization support
        /// </summary>
        /// <returns></returns>
        private static Type[] GetKnownTypes()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.StartsWith("DynamicClasses"));

            if (assembly == null)
            {
                return new Type[0];
            }

            return assembly.GetTypes().Where(t => t.Name.StartsWith("DynamicClass")).ToArray();
        }
    }
}