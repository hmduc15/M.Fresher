using System.ComponentModel.DataAnnotations;
using MISA.WebFresher042023.Demo.Core.Resources;

namespace MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset
{
    public class AssetDto
    {
     
        /// <summary>
        ///  Id Asset
        /// </summary>
        [Required]
        public Guid AssetId { get; set; }

        /// <summary>
        /// Code Asset
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string? AssetCode { get; set; }

        /// <summary>
        /// Name Asset
        /// </summary>
        public string? AssetName { get; set; }

        /// <summary>
        /// Id Department
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Code department
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Name department
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Depreciationrate Asss
        /// </summary>
        public float? DepreciationRate { get; set; }

        /// <summary>
        /// Id category
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Code category
        /// </summary>
        public string? CategoryCode { get; set; }

        /// <summary>
        /// Name category
        /// </summary>
        public string? CategoryName { get; set; }

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
        public int? Quantity { get; set; }

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
        /// Person created Asset
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Person modified Asset
        /// </summary>
        public string? ModifiedBY { get; set; }


    }
}
