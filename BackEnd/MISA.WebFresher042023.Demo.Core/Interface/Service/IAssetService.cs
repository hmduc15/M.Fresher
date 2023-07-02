using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using MISA.WebFresher042023.Demo.Core.Entity;


namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{

    /// <summary>
    /// Interface work Controller
    /// </summary>
    public interface IAssetService :IBaseService<AssetDto, AssetInsertDto,AssetUpdateDto>
    {

        /// <summary>
        /// Function Get Entity by Code 
        /// </summary>
        /// <returns>AssetCode</returns>
        /// Author: HMDUC (19/06/2023)
        Task<string> GetNewCodeAsync();


        /// <summary>
        /// Function Get List Asset Pagging 
        /// </summary>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageNumber">pageNumer</param>
        /// <param name="searchInput">search</param>
        /// <param name="m_DepartmentName">DepartmentName</param>
        /// <param name="m_CategoryName">CategoryName</param>
        /// <returns>
        ///  Object {ListAsset, totalPage}
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        Task<object> GetPagging(int pageSize, int pageNumber, string searchInput, string m_DepartmentName, string m_CategoryName);

        /// <summary>
        /// Funtion get ListAsset to Export Excel
        /// </summary>
        /// <returns></returns>
        /// Author: HMDUC (29/06/2023)
        Task<Stream> ExportExcel();
    }


}
