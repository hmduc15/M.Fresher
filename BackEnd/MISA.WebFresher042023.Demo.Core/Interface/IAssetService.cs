﻿namespace MISA.WebFresher042023.Demo.Application.Interface
{

    public interface IAssetService : IBaseService<AssetDto, AssetInsertDto, AssetUpdateDto>
    {

        /// <summary>
        /// Hàm lấy mã tài sản
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Author: HMDUC (19/06/2023)
        Task<string> GetNewCodeAsync();

        /// <summary>
        /// Hàm phân trang và tìm kiếm
        /// </summary>
        /// <param name="pageSize">Số bản ghi</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="searchInput">Từ khóa tìm kiếm</param>
        /// <param name="m_DepartmentName">Tên phòng ban sử dụng</param>
        /// <param name="m_CategoryName">Tên mã loại tài sản</param>
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
        /// Ham thực hiện Export excel
        /// </summary>
        /// Author: HMDUC (29/06/2023)
        Task<Stream> ExportExcel();


    }


}
