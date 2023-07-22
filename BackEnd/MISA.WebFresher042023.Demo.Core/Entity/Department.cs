using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Core.Entity
{
    /// <summary>
    /// Declare Departmen Entity
    /// Author: HMDUC (14/06/2023)
    /// </summary>
    public class Department : BaseEntity
    {
        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã bộ phận sử dụng
        /// </summary>

        [Required]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        /// 

        [Required]
        public string DepartmentName { get; set; } 

        /// <summary>
        /// Mô tả 
        /// </summary>
        public string? Description { get; set; }


    }
}
