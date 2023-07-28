using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application
{
    public class CategoryDto
    {
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
        /// Tên loại tài sản
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn theo loại tài sản
        /// </summary>
        public float? DepreciationRate { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? LifeTime { get; set; }

        /// <summary>
        ///  Mô tả
        /// </summary>
        /// 
        public string? Description { get; set; }

    }
}
