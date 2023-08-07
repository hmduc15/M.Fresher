using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MISA.WebFresher042023.Demo.Domain.MISAAttribute.CustomAttribute;

namespace MISA.WebFresher042023.Demo.Application
{
    public class AssetTranferDto
    {
        /// <summary>
        ///  Id tài sản
        /// </summary>
        [Required]
        public Guid AssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string? AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string? AssetName { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string? DepartmentName { get; set; }


        /// <summary>
        /// Id của bộ phận điều chuyển
        /// </summary>
        public Guid? DepartmentReceiptId { get; set; }

        /// <summary>
        /// Tên bộ phận điều chuyển
        /// </summary>
        public string? DepartmentReceipt { get; set; }

        /// <summary>
        /// Bộ phận sử dụng cũ của chứng từ
        /// </summary>
        public string? OldDepartmentName { get; set; }
      
        /// <summary>
        /// Id của bộ phận cũ
        /// </summary>
        public Guid? OldDepartmentId { get; set; }

        /// <summary>
        /// Nguyên giá của tài sản
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Số lượng tài sản
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Năm theo dõi tài sản trên phần mềm
        /// </summary>
        public int TrackedYear { get; set; }

        /// <summary>
        /// Hao mòn năm
        /// </summary>
        public Decimal DepreciationYear { get; set; }

        /// <summary>
        /// Hao mòn/ khấu hao lũy kế
        /// </summary>
        public Decimal DepreciationAmount { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public Decimal ResidualPrice { get; set; }

        /// <summary>
        /// Số năm sử dụng của tài sản
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        public string?  Reason { get; set; }
    }

}
