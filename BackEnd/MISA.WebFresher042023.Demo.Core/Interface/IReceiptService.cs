using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application.Interface
{
    /// <summary>
    /// Interface Receipt Service
    /// </summary>
    /// Author: HMDUC (26/07/2023)
    public interface IReceiptService : IBaseService<ReceiptDto,ReceiptInsertDto,ReceiptUpdateDto,ReceiptTranferDto>
    {
        /// <summary>
        /// Hàm lấy ra mã chứng từ mới nhất phục vụ thêm mới chứng từ
        /// </summary>
        /// <returns>Mã chứng từ</returns>
        ///  Author: HMDUC (27/07/2023)
        Task<string> GetNewCodeAsync();

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


        /// <summary>
        /// Hàm thêm mơi chứng từ
        /// </summary>
        /// <param name="receipt">Chứng từ</param>
        /// <param name="listAsset">Danh sách tài sản có trong chứng từ</param>
        ///  Author: HMDUC (27/07/2023)
        Task<int> InsertReceiptAndAssetAsync(ReceiptInsertDto receiptInsertDto, List<ReceiptAsset> listAsset, List<MemberReceipt> listMembers);

        /// <summary>
        /// Hàm cập nhật lại chứng từ
        /// </summary>
        /// <param name="receipt">chứng từ cần cập nhật</param>
        /// <param name="receiptAssets"></param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// Author: HMDUC (03/08/2023)
        Task<int> UpdateReceiptAndAssetAsync(ReceiptUpdateDto receiptUpdateDto, List<ReceiptAsset> receiptAssets, List<MemberReceipt> listMembers,List<ReceiptAsset> listDelete);

        /// <summary>
        /// Hàm xóa một chứng từ theo Id
        /// </summary>
        /// <param name="id">id chứng từ</param>
        /// <returns>Số dòng bị ảnh hưởng </returns>
        /// Author: HMDUC (06/08/2023)
        Task<int> DeleteOnlyReceiptAsync(Guid id);

        /// <summary>
        /// Hàm check nghiệp vụ xóa chứng từ 
        /// </summary>
        /// <param name="receiptId">Id của chứng từ</param>
        /// <returns>Obj</returns>
        /// Author: HMDUC (06/08/2023)
        Task<Object> CheckReference(Guid receiptId);

        /// <summary>
        ///  Hàm check nghiệp vụ phục xóa tài sản ở FORM EDIT
        /// </summary>
        /// <param name="assetId">Id của tài sản</param>
        /// <param name="receiptId">Id của chứng từ</param>
        /// <returns>Obj</returns>
        /// Author: HMDUC (06/08/2023)
        Task<Object> CheckExistReceiptOfAsset(Guid assetId, Guid receiptId);

        /// <summary>
        /// Hàm check nghiệp vụ xóa nhiều
        /// </summary>
        /// <param name="ids">Danh sách ID của chứng từ</param>
        /// <returns>Obj</returns>
        /// Author: HMDUC (06/08/2023)
        Task<Object> CheckDeleteListReceipt(List<Guid> ids);
    }
}
