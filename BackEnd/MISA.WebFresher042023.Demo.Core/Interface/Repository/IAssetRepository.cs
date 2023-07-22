using MISA.WebFresher042023.Demo.Core.Dto;
using MISA.WebFresher042023.Demo.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    /// <summary>
    /// Interface Asset Repository
    /// </summary>
    public interface IAssetRepository : IBaseRepository<Asset>
    {



        public bool CheckExistAssetCode(string? assetCode, Guid? id = null);

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
        /// Hàm lấy ra danh sách tài sản phục vụ export Excel
        /// </summary>
        /// <returns></returns>
        /// Author: HMDUC (29/06/2023)
        Task<Stream> GetListExport();
    }
}
