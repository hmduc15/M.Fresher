using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Interface.Manager
{
    /// <summary>
    /// Interface các hàm check Validate Nghiệp vụ của chứng từ
    /// </summary>
    public interface  IReceiptManager
    {
        /// <summary>
        ///  Hàm check trùng mã chứng từ
        /// </summary>
        /// <param name="code">Mã chứng từ</param>
        /// <param name="receiptId">ID của chứng từ</param>
        /// <param name="isInsert">
        /// true: Method insert
        /// false: Method Update
        /// </param>
        /// Author: HMDUC (31/07/2023)
        public void CheckReceiptExistBydCode(string? code , Guid? receiptId = null, bool isInsert = true);

        /// <summary>
        /// Ham check phát sinh chứng từ theo Id chứng từ
        /// </summary>
        /// <param name="receiptId">Id chứng từ</param>
        /// Author: HMDUC (31/07/2023)
        public void CheckAccrued(Guid receiptId);

        /// <summary>
        /// Hàm check ngày chứng từ và ngày điều chuyển
        /// </summary>
        /// <param name="receiptDate">Ngày chứng từ</param>
        /// <param name="tranferDate">Ngày điều chuyển</param>
        /// Author: HMDUC (31/07/2023)
        public void CheckTime(DateTime? receiptDate, DateTime? tranferDate);

    }
}
