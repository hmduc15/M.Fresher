using MISA.WebFresher042023.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Interface
{
    public interface IReceiptRepository : IBaseRepository<Receipt>
    {
        /// <summary>
        /// hàm phân trang và tìm kiếm chứng từ
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchInput"></param>
        /// Author: HMDUC (26/07/2023)
        Task<object> GetPagging(int pageSize, int pageNumber, string searchInput);

        /// <summary>
        /// hàm phân trang list tài sản theo mã chứng từ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// Author: HMDUC (26/07/2023)
        Task<object> GetAssetByReceiptIdPagging(Guid id, int pageSize, int pageNumber);
    }
}
