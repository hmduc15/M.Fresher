using System.ComponentModel.DataAnnotations;
using MISA.WebFresher042023.Demo.Core.MISAAttribute;
using static MISA.WebFresher042023.Demo.Core.MISAAttribute.CustomAttribute;

namespace MISA.WebFresher042023.Demo.Core.Entity;

/// <summary>
///  Declare class Asset Entity
///  Author: HMDUC (11/03/2023)
/// </summary>
public class Asset : BaseEntity
{
    /// <summary>
    ///  Id Asset
    /// </summary>
    [Required]
    [ExcelColumn("AssetId")]
    public Guid AssetId { get; set; }

    /// <summary>
    /// Code Asset
    /// </summary>
    [ExcelColumn("AssetCode")]
    public string? AssetCode { get; set; }

    /// <summary>
    /// Name Asset
    /// </summary>
    [ExcelColumn("AssetName")]
    public string? AssetName { get; set; }

    /// <summary>
    /// Id Department
    /// </summary>
    [Required]
    [ExcelColumn("DepartmentId")]
    public Guid DepartmentId { get; set; }

    /// <summary>
    /// Code department
    /// </summary>
    [ExcelColumn("DepartmentCode")]
    public string? DepartmentCode { get; set; }

    /// <summary>
    /// Name department
    /// </summary>
    [ExcelColumn("DepartmentName")]
    public string? DepartmentName { get; set; }

    /// <summary>
    /// DepreciationRate
    /// </summary>
    [ExcelColumn("DepreciationRate")]
    public float DepreciationRate { get; set; }

    /// <summary>
    /// Id category
    /// </summary>
    [Required]
    [ExcelColumn("CategoryId")]
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Code category
    /// </summary>
    [ExcelColumn("CategoryCode")]
    public string? CategoryCode { get; set; }

    /// <summary>
    /// Name category
    /// </summary>
    [ExcelColumn("CategoryName")]
    public string? CategoryName { get; set; }

    /// <summary>
    /// Date buy Asset
    /// </summary>
    [ExcelColumn("PurchaseDate")]
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// Cost of Asset
    /// </summary>
    [ExcelColumn("Cost")]
    public decimal Cost { get; set; }

    /// <summary>
    /// Quantity of Asset
    /// </summary>
    [ExcelColumn("Quantity")]
    public int Quantity { get; set; }

    /// <summary>
    /// Year tracked Asset
    /// </summary>
    [ExcelColumn("TrackedYear")]
    public int TrackedYear { get; set; }

    /// <summary>
    /// Life time of Asset
    /// </summary>
    [ExcelColumn("LifeTime")]
    public int LifeTime { get; set; }

    /// <summary>
    /// Rate Deprecication / year
    /// </summary>
    [ExcelColumn("DepreciationYear")]
    public Decimal DepreciationYear { get; set; }
       
    /// <summary>
    /// Depreciation Amount
    /// </summary>
    [ExcelColumn("DepreciationAmount")]
    public Decimal DepreciationAmount { get; set; }   

    /// <summary>
    /// Residual Price
    /// </summary>
    [ExcelColumn("ResidualPrice")]
    public Decimal ResidualPrice { get; set; }

    /// <summary>
    /// Year use Asset
    /// </summary>
    [ExcelColumn("ProductionYear")]
    public DateTime? ProductionYear { get; set; }

}
