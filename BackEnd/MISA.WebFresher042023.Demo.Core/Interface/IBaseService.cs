using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application.Interface
{
    public interface IBaseService<TEntityDto, TEntityInsertDto, TEntityUpdateDto>
    {
        /// <summary>
        ///  Hàm lấy tất cả Entity có trong Db
        /// </summary>
        /// <returns>List Entity</returns>
        /// Author: HMDUC (16/06/2023)
        Task<List<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Hàm lấy ra Entity theo Id
        /// </summary>
        /// <param name="id">Id của Entity</param>
        /// <returns>Entity</returns>
        /// Author: HMDUC (16/06/2023)
        Task<TEntityDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Hàm thêm mới Entity
        /// </summary>
        /// <param name="entityInsert">Entity thêm mới nhận từ Controller</param>
        /// <returns>
        /// Số dòng bị ảnh hưởng trong Db
        /// </returns>
        /// Author: HMDUC (16/06/2023)
        Task<int> InsertAsync(TEntityInsertDto entityInsert);

        /// <summary>
        /// Hàm cập nhật Entity
        /// </summary>
        /// <param name="entityUpdateD">Entity cần cập nhật</param>
        /// <returns>
        /// Số dòng bị ảnh hưởng trong Db
        /// </returns>
        ///  Author: HMDUC (16/06/2023)
        Task<int> UpdateAsync(TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Hàm xóa Entity theo Id
        /// </summary>
        /// <param name="id">Id của Entity cần xóa</param>
        /// <returns>
        ///  Số dòng bị ảnh hưởng trong Db
        /// </returns>
        /// Author: HMDUC (16/06/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Hàm xóa nhiều Entity
        /// </summary>
        /// <param name="assetIds">Danh sách Entity cần xóa</param>
        /// <returns>
        /// Count Row Affect
        /// </returns>
        /// Author: HMDUC (16/06/2023)
        Task<int> DeleteMulAsync(List<Guid> ids);



    }
}
