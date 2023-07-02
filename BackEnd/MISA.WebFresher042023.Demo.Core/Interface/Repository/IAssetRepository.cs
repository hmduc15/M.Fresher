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
        /// <summary>
        /// Funtion get Entity Code of new Enity 
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

        Task<Stream> GetListExport();
    }
}
