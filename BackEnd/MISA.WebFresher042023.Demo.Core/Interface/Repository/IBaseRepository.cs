using MISA.WebFresher042023.Demo.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    /// <summary>
    /// Interface base Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        ///  Function Get list Record
        /// </summary>
        /// <returns>List Asset</returns>
        /// Author: HMDUC (16/06/2023)
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Function  Get Record by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Entity</returns>
        /// Author: HMDUC (16/06/2023)
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        ///  Functio Get  by Code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        /// Author: HMDUC (16/06/2023)
        Task<TEntity> GetByCodeAsync(string code);

       

        /// <summary>
        /// Function Insert a record 
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>
        /// New Entity
        /// </returns>
        /// Author: HMDUC (16/06/2023)
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Function Update Asset
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>
        ///  Asset
        /// </returns>
        ///  Author: HMDUC (16/06/2023)
        Task<int> UpdateAsync(TEntity entity);


        /// <summary>
        /// Function Delete  by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        ///  Count Row Affect
        /// </returns>
        /// Author: HMDUC (16/06/2023)

        Task<int> DeleteEntityAsync(Guid id);

        /// <summary>
        /// Function Delete Multiple 
        /// </summary>
        /// <param name="ids">List Id</param>
        /// <returns>
        /// Count Row Affect
        /// </returns>
        /// Author: HMDUC (16/06/2023)
        Task<int> DeleteEntityMulAsync(List<Guid> ids);

      

    }
}
