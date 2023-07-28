using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Entity
{  
    /// <summary>
    /// Entity chứng từ điều chuyển
    /// </summary>
    public class Receipt : BaseEntity
    {
        /// <summary>
        /// Id của chứng từ
        /// </summary>
        [Required]
        public Guid ReceiptId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        public string? ReceiptCode { get; set; }

        /// <summary>
        /// Ngày lập chứng từ
        /// </summary>
        [Required]
        public DateTime ReceiptDate { get; set; }

        /// <summary>
        /// Ngày điều chuyển tài sản
        /// </summary>
        [Required]
        public DateTime TranferDate { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }
       
        /// <summary>
        /// Tổng nguyên giá của tất cả tài sản có trong chứng từ
        /// </summary>
        public Decimal OrgPrice { get; set; }

        /// <summary>
        /// Tổng giá trị của tất cả tài sản có trong chứng từs
        /// </summary>
        public Decimal ResidualPrice { get; set; }
    }
}
