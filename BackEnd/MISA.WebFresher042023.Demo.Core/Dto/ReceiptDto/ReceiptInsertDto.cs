using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application;
/// <summary>
/// Entity Dto 
/// </summary>
public class ReceiptInsertDto
{
    /// <summary>
    /// Mã chứng từ
    /// </summary>
    public string? ReceiptCode { get; set; }

    /// <summary>
    /// Ngày lập chứng từ
    /// </summary>
    [Required]
    public DateTime ReceiptDate { get; set; }

    /// <summary>
    /// Ngày điều chuyển tài sản
    /// </summary>
    [Required]
    public DateTime TranferDate { get; set; }

    /// <summary>
    /// Ghi chú
    /// </summary>
    public string? Note { get; set; }
}

