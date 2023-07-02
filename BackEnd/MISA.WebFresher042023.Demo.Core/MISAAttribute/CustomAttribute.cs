using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.MISAAttribute
{
    /// <summary>
    /// Custome Attribute for Project
    /// </summary>
    public class CustomAttribute {

        /// <summary>
        /// Custom Attribute for Export Excel
        /// </summary>
        /// Author: HMDUC (01/07/2023)
        [AttributeUsage(AttributeTargets.Property)]
        public class ExcelColumnAttribute : Attribute
        {
            /// <summary>
            /// Tên cột
            /// </summary>
            public string ColumnName { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="columnName">Column Name</param>
            /// Author: HMDUC (01/07/2023)
            public ExcelColumnAttribute(string columnName)
            {
                ColumnName = columnName;
            }
        }
    }
}
