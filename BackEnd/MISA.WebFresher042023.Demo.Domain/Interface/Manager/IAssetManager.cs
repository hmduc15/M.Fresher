using MISA.WebFresher042023.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Interface.Manager
{
    /// <summary>
    /// Interface khai báo các hàm Validate Nghiệp vụ của Asset
    /// </summary>
    public interface IAssetManager
    {
        /// <summary>
        /// Hàm check trùng mã tài sản
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <param name="id">Id tài sản</param>
        /// <param name="isInsert">Method là Insert hay Updte</param>
        /// Author: HMDUC (20/07/2023)
        public void CheckAssetExistByCode(string? code, Guid? id = null, bool isInsert = true);

        /// <summary>
        /// Hàm check ngày mua và ngày bắt đầu sử dụng của tài sản
        /// </summary>
        /// <param name="purchaseDate">Ngày mua</param>
        /// <param name="productionYear">Ngày bắt đầu sử dụng</param>
        /// Author: HMDUC (20/07/2023)
        public void CheckTime(DateTime? purchaseDate, DateTime? productionYear);

        /// <summary>
        /// Hàm check tỷ lệ hao mòn và giá trị hao mòn năm
        /// </summary>
        /// <param name="depreciationRate">Tỷ lệ hao mòn</param>
        /// <param name="depreciationYear">Hao mòn năm</param>
        /// <param name="cost">Nguyên giá</param>
        /// Author: HMDUC (20/07/2023)
        public void CheckDepreciation(float depreciationRate, decimal depreciationYear, decimal cost, int lifeTime);
    }
}
