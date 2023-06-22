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
        /// Id Departmnet
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Code Department
        /// </summary>

        [Required]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Name Department
        /// </summary>
        /// 

        [Required]
        public string DepartmentName { get; set; } 

        /// <summary>
        /// Description 
        /// </summary>
        public string? Description { get; set; }


    }
}
