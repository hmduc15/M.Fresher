using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Demo.Core.Resources;

namespace MISA.WebFresher042023.Demo.Core.MISAAttribute
{
    /// <summary>
    /// Custome Attribute for Project
    /// </summary>
    public class CustomAttribute
    {

        /// <summary>
        /// Custom Attribute phục vụ Export Excel
        /// </summary>
        /// Author: HMDUC (01/07/2023)
        [AttributeUsage(AttributeTargets.Property)]
        public class ExcelColumnAttribute : Attribute
        {
            /// <summary>
            /// Tên cột
            /// </summary>
            public string ColumnName { get;}

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="columnName">Column Name</param>
            /// Author: HMDUC (01/07/2023)
            public ExcelColumnAttribute(string columnName)
            {
                ColumnName = GetColumnNameResource(columnName);
            }

            /// <summary>
            /// Hàm lấy ra resource theo key được truyền vào Attribute
            /// </summary>
            /// <param name="resourceKey"></param>
            /// Author: HMDUC (01/07/2023)
            /// <returns>resoure value</returns>
            private string GetColumnNameResource(string resourceKey)
            {
                return Resources.ExportVN.ResourceManager.GetString(resourceKey);

            }
        }
    }
}
