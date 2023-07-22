using MISA.WebFresher042023.Demo.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset
{
    public class AssetInsertDto
    {
        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string? AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string? AssetName { get; set; }

        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

       /// <summary>
       /// Tỷ lệ hao mòn của tài sản
       /// </summary>
        public float? DepreciationRate { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Ngày mua tài sản
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal? Cost { get; set; }

        /// <summary>
        /// Hao mòn năm của tài sản
        /// </summary>
        public Decimal DepreciationYear { get; set; }

        /// <summary>
        /// Số lượng của tài sản
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Năm theo dõi tài sản trên phần mềm
        /// </summary>
        public int? TrackedYear { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? LifeTime { get; set; }

        /// <summary>
        ///  NĂm sử dụng
        /// </summary>
        public DateTime? ProductionYear { get; set; }

    }
}
