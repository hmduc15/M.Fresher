using System.ComponentModel.DataAnnotations;
using static MISA.WebFresher042023.Demo.Domain.MISAAttribute.CustomAttribute;

namespace MISA.WebFresher042023.Demo.Domain.Entity;

/// <summary>
///  Declare class Asset Entity
///  Author: HMDUC (11/03/2023)
/// </summary>
public class Asset : BaseEntity
{
    /// <summary>
    ///  Id tài sản
    /// </summary>
    [Required]
    [ExcelColumn("AssetId")]
    public Guid AssetId { get; set; }

    /// <summary>
    /// Mã tài sản
    /// </summary>
    [ExcelColumn("AssetCode")]
    public string? AssetCode { get; set; }

    /// <summary>
    /// Tên tài sản
    /// </summary>
    [ExcelColumn("AssetName")]
    public string? AssetName { get; set; }

    /// <summary>
    /// Id bộ phạn sử dụng
    /// </summary>
    [Required]
    [ExcelColumn("DepartmentId")]
    public Guid DepartmentId { get; set; }

    /// <summary>
    /// Mã bộ phận sử dụng
    /// </summary>
    [ExcelColumn("DepartmentCode")]
    public string? DepartmentCode { get; set; }

    /// <summary>
    /// Tên bộ phận sử dụng
    /// </summary>
    [ExcelColumn("DepartmentName")]
    public string? DepartmentName { get; set; }

    /// <summary>
    /// Tỷ lệ hao mòn
    /// </summary>
    [ExcelColumn("DepreciationRate")]
    public float DepreciationRate { get; set; }

    /// <summary>
    /// Id loại tài sản
    /// </summary>
    [Required]
    [ExcelColumn("CategoryId")]
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Mã loại tài sản
    /// </summary>
    [ExcelColumn("CategoryCode")]
    public string? CategoryCode { get; set; }

    /// <summary>
    /// Tên tài sản
    /// </summary>
    [ExcelColumn("CategoryName")]
    public string? CategoryName { get; set; }

    /// <summary>
    /// Ngày mua tài sản
    /// </summary>
    [ExcelColumn("PurchaseDate")]
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// Nguyên giá
    /// </summary>
    [ExcelColumn("Cost")]
    public decimal Cost { get; set; }

    /// <summary>
    /// Số lượng
    /// </summary>
    [ExcelColumn("Quantity")]
    public int Quantity { get; set; }

    /// <summary>
    /// NĂm theo dõi
    /// </summary>
    [ExcelColumn("TrackedYear")]
    public int TrackedYear { get; set; }

    /// <summary>
    /// Số năm sử dụng 
    /// </summary>
    [ExcelColumn("LifeTime")]
    public int LifeTime { get; set; }

    /// <summary>
    /// Hao mòn năm
    /// </summary>
    [ExcelColumn("DepreciationYear")]
    public Decimal DepreciationYear { get; set; }
       
    /// <summary>
    /// Hao mòn/ khấu hao lũy kế
    /// </summary>
    [ExcelColumn("DepreciationAmount")]
    public Decimal DepreciationAmount { get; set; }   

    /// <summary>
    /// Giá trị còn lại
    /// </summary>
    [ExcelColumn("ResidualPrice")]
    public Decimal ResidualPrice { get; set; }

    /// <summary>
    ///  Năm sử dụng
    /// </summary>
    [ExcelColumn("ProductionYear")]
    public DateTime? ProductionYear { get; set; }

}
