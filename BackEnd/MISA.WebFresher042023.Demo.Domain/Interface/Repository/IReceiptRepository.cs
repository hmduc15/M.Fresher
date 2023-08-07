using MISA.WebFresher042023.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Interface.Repository
{
    public interface IReceiptRepository : IBaseRepository<Receipt>
    {
        /// <summary>
        /// Hàm check trùng mã chứng từ 
        /// </summary>
        /// <param name="receiptCode">Mã chứng từ cần check</param>
        /// <param name="receiptId">Id của chứng từ cần check</param>
        /// <returns>
        /// true: Trùng 
        /// false: Không trùng
        /// </returns>
        /// Author: HMDUC (27/07/2023)
        public bool CheckIsDuplicate(string? receiptCode, Guid? receiptId = null);

        /// <summary>
        ///  Hàm check chứng từ phát sinh
        /// </summary>
        /// <param name="receiptId">Chứng từ cần check</param>
        /// <returns>
        /// true: có phát sinh
        /// false: không phát sinh
        /// </returns>
        /// Author: HMDUC (27/07/2023)
        public bool CheckIsAccrued(Guid? receiptId);

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
        /// Hàm lấy ra danh sách chứng từ phát sinh chung
        /// </summary>
        /// <param name="ids">Danh sách Id của chứng từ</param>
        /// <returns>
        /// Danh sách chứng từ
        /// </returns>
        /// Author: HMDUC (05/08/2023)
        Task<List<ReceiptAssetCommon>> GetListAssetCommonReceipId(List<Guid> ids);

        /// <summary>
        /// Hàm thêm mơi chứng từ
        /// </summary>
        /// <param name="receipt">Chứng từ</param>
        /// <param name="listAsset">Danh sách tài sản có trong chứng từ</param>
        ///  Author: HMDUC (27/07/2023)
        Task<int> InsertReceiptAndAssetAsync(Receipt receipt, List<ReceiptAsset> listAsset, List<MemberReceipt> memberReceipts);

        /// <summary>
        /// Hàm cập nhật lại chứng từ
        /// </summary>
        /// <param name="receipt">chứng từ cần cập nhật</param>
        /// <param name="receiptAssets"></param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// Author: HMDUC (03/08/2023)
        Task<int> UpdateReceiptAndAssetAsync(Receipt receipt, List<ReceiptAsset> receiptAssets, List<MemberReceipt> memberReceipts, List<ReceiptAsset> listDelete);

        /// <summary>
        ///  Hàm lấy ra chứng từ phát sinh theo Id
        /// </summary>
        /// <param name="assetId">Danh sách id tài sản của chứng từ</param>
        /// <param name="receiptId">ID chứng từ</param>
        /// <returns>Danh sách chứng từ phát sinh</returns>
        /// Author: HMDUC (27/07/2023)
        Task<List<Receipt>> GetReceiptAccured(List<Guid> ids, Guid receiptId);

        /// <summary>
        /// Hàm xóa chứng từ
        /// </summary>
        /// <param name="listAsset"></param>
        /// <param name="id"></param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        ///  Author: HMDUC (27/07/2023)
        Task<int> DeleteOnlyReceiptAsync(Guid id);

        /// <summary>
        /// Hàm lấy danh sách tài sản theo Id chứng từ
        /// </summary>
        /// <param name="id">Id chứng từ</param>
        /// <returns>DS tài sản của chứng từ</returns>
        /// Author: HMDUC (27/07/2023)
        Task<List<Asset>> GetAssetByReceiptId(Guid id);

        /// <summary>
        /// Hàm lấy chứng từ theo AssetId và khác ID chứng từ truyền vào phục vụ nghiệp vụ xóa tài sản ở Form Edit
        /// </summary>
        /// <param name="assetId">Id Tài sản</param>
        /// <param name="receiptId">Id chứng từ</param>
        /// <returns>Danh sách chứng từ</returns>
        /// Author: HMDUC (06/08/2023)
        Task<List<ReceiptAssetCommon>> GetAllReceiptByAssetId(Guid assetId, Guid receiptId);

        /// <summary>
        ///     
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<ReceiptAssetCommon>> GetListAssetByListReceiptId(List<Guid> ids);

        /// <summary>
        /// Hàm lẩy ra danh sách tài sản khác với  danh sách Id của chứng từ
        /// </summary>
        /// <param name="ids">Danh sách Id của chứng từ </param>
        /// <returns>Danh sách tài sản</returns>
        /// Author: HMDUC (07/08/2023)
        Task<List<ReceiptAssetCommon>> GetListAssetByNotListReceiptId(List<Guid> ids);
    }
}
