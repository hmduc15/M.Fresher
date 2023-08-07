using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Entity
{
    public class ReceiptAssetCommon
    {
    
        /// <summary>
        /// ID của chứng từ
        /// </summary>
        public Guid ReceiptId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        public string? ReceiptCode { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        public DateTime ReceiptDate { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string? AssetName { get; set; }

        /// <summary>
        /// ID của tài sản
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Ngày lập chứng từ
        /// </summary>
        public DateTime CreatedDate { get; set; }   

    }
}
