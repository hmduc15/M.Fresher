using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Core.Entity
{
    /// <summary>
    /// Declare Category Entity
    /// Author: HMDUC (15/6/2023)
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id Category
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Code Category
        /// </summary>
        public string? CategoryCode { get; set; }

        /// <summary>
        /// Name Category
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// DepreciationRate Category Asset
        /// </summary>
        public float? DepreciationRate { get; set; }

        /// <summary>
        /// LifeTime
        /// </summary>
        public int? LifeTime { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// 
        public string? Description { get; set; }

        /// <summary>
        /// Person created Category
        /// </summary>
        /// 
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Date created Category
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Person modified Category
        /// </summary>
        public string? ModifiedBY { get; set; }

        /// <summary>
        /// Date modified Category
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
