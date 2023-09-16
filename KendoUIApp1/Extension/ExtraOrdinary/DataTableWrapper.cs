using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace KendoUIApp1.Extension.ExtraOrdinary
{
    internal class DataTableWrapper : IEnumerable<DataRowView>, IEnumerable
    {
        public DataTable Table { get; private set; }

        internal DataTableWrapper(DataTable dataTable)
        {
            Table = dataTable;
        }

        public IEnumerator<DataRowView> GetEnumerator()
        {
            if (Table == null)
            {
                yield break;
            }

            foreach (DataRowView item in Table.AsDataView())
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}