using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Domain.Entity
{
    /// <summary>
    /// Loại tài sản
    /// Author: HMDUC (15/6/2023)
    /// </summary>
    public class Category
    {
        /// <summary>
        ///  Id loại tài sản
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string? CategoryCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn của loại tài sản
        /// </summary>
        public float? DepreciationRate { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        public int? LifeTime { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        /// 
        public string? Description { get; set; }

        /// <summary>
        /// Người tạo mã loại tài sản
        /// </summary>
        /// 
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo mã loại tài sản
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người sửa mã loại tài sản
        /// </summary>
        public string? ModifiedBY { get; set; }

        /// <summary>
        /// Ngày sửa mã loại tài sản
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
