using MISA.WebFresher042023.Demo.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset
{
    public class AssetUpdateDto
    {  

        /// <summary>
        /// Id Asset
        /// </summary>
        public  Guid AssetId{ get; set; }

        /// <summary>
        /// Code Asset
        /// </summary>
        public string? AssetCode { get; set; }

        /// <summary>
        /// Name Asset
        /// </summary>
        public string? AssetName { get; set; }

  
        /// <summary>
        /// Id department
        /// </summary>
        public Guid DepartmentId { get; set; }


        /// <summary>
        /// Depreciation
        /// </summary>
        public float? DepreciationRate { get; set; }
        
        /// <summary>
        /// Depreciation Year
        /// </summary>
        public Decimal DepreciationYear { get; set; }

        /// <summary>
        /// Id category
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }


        /// <summary>
        /// Date buy Asset
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Cost of Asset
        /// </summary>
        public decimal? Cost { get; set; }

        /// <summary>
        /// Quantity of Asset
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Year tracked Asset
        /// </summary>
        public int? TrackedYear { get; set; }

        /// <summary>
        /// Life time of Asset
        /// </summary>
        public int? LifeTime { get; set; }

        /// <summary>
        /// Year use Asset
        /// </summary>
        public DateTime? ProductionYear { get; set; }

        /// <summary>
        /// Time update
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

    }
}
