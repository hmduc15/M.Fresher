using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Interface.Repository
{
    /// <summary>
    /// Interface base Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        ///  Hàm lấy ra danh sách tài sản từ DB
        /// </summary>
        /// Author: HMDUC (16/06/2023)
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Hàm lấy ra tài sản theo Id
        /// </summary>
        /// <param name="Id">Id của tài sản</param>
        /// Author: HMDUC (16/06/2023)
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// Hàm lấy ra tài sản theo mã loại tài sản
        /// </summary>
        /// <param name="Code">Mã tài sản</param>
        /// <returns></returns>
        /// Author: HMDUC (16/06/2023)
        Task<TEntity> GetByCodeAsync(string code);

        /// <summary>
        /// Hàm thêm mới tài sản
        /// </summary>
        /// <param name="TEntity">Tài sản</param>
        /// Author: HMDUC (16/06/2023)
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Hàm cập nhật tài sản
        /// </summary>
        /// <param name="TEntity">Tài sản được cập nhật</param>
        ///  Author: HMDUC (16/06/2023)
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// Hàm xóa tài sản theo Id
        /// </summary>
        /// <param name="Id">Id của tài sản</param>
        /// Author: HMDUC (16/06/2023)
        Task<int> DeleteEntityAsync(Guid id);

        /// <summary>
        /// Hàm xóa nhiều tài sản
        /// </summary>
        /// <param name="ids">Danh sạch Id của tài sản</param>
        /// Author: HMDUC (16/06/2023)
        Task<int> DeleteEntityMulAsync(List<Guid> ids);



    }
}
