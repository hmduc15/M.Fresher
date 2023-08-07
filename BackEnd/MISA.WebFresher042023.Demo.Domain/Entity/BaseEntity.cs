using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Entity
{
    public abstract class BaseEntity
    {

        /// <summary>
        /// Người thêm tài sản
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày thêm tài sản
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        ///  Người sửa tài sản
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Thời gian sửa tài sản
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

    }
}
