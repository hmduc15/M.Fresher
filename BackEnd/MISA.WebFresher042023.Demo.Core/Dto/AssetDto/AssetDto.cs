using System.ComponentModel.DataAnnotations;
using MISA.WebFresher042023.Demo.Core.Resources;

namespace MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset
{
    public class AssetDto
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
        /// Id bộ phận sử dụng
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã bộ phận sử dụng
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn tài sanr
        /// </summary>
        public float? DepreciationRate { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string? CategoryCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        ///  Ngày mua tài sản
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Nguyên giá của tài sản
        /// </summary>
        public decimal? Cost { get; set; }

        /// <summary>
        /// Số lượng tài sản
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Năm theo dõi tài sản trên phần mềm
        /// </summary>
        public int? TrackedYear { get; set; }

        /// <summary>
        /// Số năm sử dụng của tài sản
        /// </summary>
        public int? LifeTime { get; set; }

        /// <summary>
        /// Năm sử dụng tài sản
        /// </summary>
        public DateTime? ProductionYear { get; set; }

        /// <summary>
        /// Người thêm tài sản
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian thêm tài sản
        /// </summary>
        public string? ModifiedBY { get; set; }


    }
}
