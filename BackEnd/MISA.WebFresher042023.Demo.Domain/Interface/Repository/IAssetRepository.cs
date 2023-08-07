using MISA.WebFresher042023.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Interface.Repository
{
    /// <summary>
    /// Interface Asset Repository
    /// </summary>
    public interface IAssetRepository : IBaseRepository<Asset>
    {

        /// <summary>
        /// Hàm check trùng mã tài sản
        /// </summary>
        /// <param name="assetCode">Mã tài sản cần check</param>
        /// <param name="id">id của mã tài sản</param>
        /// <returns>
        /// True:  trùng
        /// False: không trùng
        /// </returns>
        /// Author: HMDUC (20/07/2023)
        public bool CheckExistAssetCode(string? assetCode, Guid? id = null);

        /// <summary>
        /// Hàm lấy tất cả các tài sản theo mã chứng từ
        /// </summary>
        /// <param name="receiptId">Mã chứng từ</param>
        /// <returns>
        /// Danh sách tài sản
        /// </returns>
        /// Author: HMDUC (28/07/2023)
        Task<List<Asset>> GetAssetAllByReceipId(Guid receiptId);

        /// <summary>
        /// Hàm lấy ra mã tài sản mới từ db 
        /// </summary>
        /// <returns>AssetCode</returns>
        /// Author: HMDUC (19/06/2023)
        Task<string> GetNewCodeAsync();

        /// <summary>
        /// Hàm phân trang + tìm kiếm  
        /// </summary>
        /// <param name="pageSize">Số dòng</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="searchInput">Mã tài sản hoặc tên tài sản</param>
        /// <param name="m_DepartmentName">Tên bộ phận sử dụng</param>
        /// <param name="m_CategoryName">Tên loại tài sản</param>
        /// <returns>
        ///  Object {ListAsset, totalPage}
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        Task<object> GetPagging(int pageSize, int pageNumber, string searchInput, string m_DepartmentName, string m_CategoryName);

        /// <summary>
        /// Hàm phân trang + tìm kiếm tài sản phục vụ thêm tài sản vào chứng từ
        /// </summary>
        /// <param name="ids">Danh sach Id tài sản cần loại bỏ</param>
        /// <param name="pageSize">Số dòng hiển thị</param>
        /// <param name="pageNumber">Số trang</param>
        /// Author: HMDUC (28/07/2023)
        Task<object> GetPaggingAssetChose(List<Guid> ids, int pageSize, int pageNumber);

        /// <summary>
        /// Hàm lấy ra danh sách tài sản phục vụ export Excel
        /// </summary>
        /// <returns></returns>
        /// Author: HMDUC (29/06/2023)
        Task<Stream> GetListExport();


        /// <summary>
        /// Hàm check chứng từ phát sinh của danh sách tài sản
        /// </summary>
        /// <param name="ids">Danh sách Id của tài sản</param>
        /// <returns>
        /// Danh sách chứng từ
        /// </returns>
        /// Author: HMDUC (05/08/2023)
        Task<List<ReceiptAssetCommon>> GetReceiptByAssetId(List<Guid> ids);

    }
}
