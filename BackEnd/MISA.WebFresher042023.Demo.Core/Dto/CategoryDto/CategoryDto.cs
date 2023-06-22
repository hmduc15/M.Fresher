using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.CategoryDto
{
    public class CategoryDto
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

    }
}
