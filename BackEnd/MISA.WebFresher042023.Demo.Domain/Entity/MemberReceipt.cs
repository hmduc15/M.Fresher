using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Entity;

public class MemberReceipt : BaseEntity
{

    /// <summary>
    /// Id ban giao nhận 
    /// </summary>
    public Guid MemberId { get; set; }

    /// <summary>
    /// Họ tên 
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Số thứ tự 
    /// </summary>
    public int STT { get; set; }

    /// <summary>
    /// Đai diện ban giao nhận 
    /// </summary>
    public string? Representation { get; set; }

    /// <summary>
    /// Chức vụ
    /// </summary>
    public string? PositionName { get; set; }

  
}



