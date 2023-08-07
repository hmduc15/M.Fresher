using MISA.WebFresher042023.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Interface.Repository
{
    public interface IMemberRepository : IBaseRepository<MemberReceipt>
    {
        /// <summary>
        /// Hàm lấy ra ds ban giao nhận trước đó
        /// </summary>
        /// <returns>Danh sách ban giao nhận</returns>
        /// Author: HMDUC (05/08/2023)
        Task<List<MemberReceipt>> GetPrevMemberAsync();

        /// <summary>
        /// Hàm lấy ra ds ban giao nhận theo Id của chừn từ
        /// </summary>
        /// <param name="receiptId">Id chứng từ</param>
        /// <returns>Danh sách ban giao nhận</returns>
        /// Author: HMDUC(06/08/2023)
        Task<List<MemberReceipt>> GetMemberByReceiptIdAsync(Guid receiptId);
    }
}
