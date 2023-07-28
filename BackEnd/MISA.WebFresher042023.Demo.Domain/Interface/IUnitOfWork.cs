using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Hàm mở connection Db
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        DbConnection Connection { get; }

        /// <summary>
        /// Hàm  transaction
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        DbTransaction? Transaction { get; }

        /// <summary>
        /// Hàm mở transaction
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        void BeginTransaction();

        /// <summary>
        /// Hàm bất đồng bộ mở transaction
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        Task BeginTrasactionAsync();

        /// <summary>
        /// Hàm commit sau khi truy vấn thành công
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        void Commit();

        /// <summary>
        /// Hàm bất đồng bộ commit sau khi truy vấn thành công
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        Task CommitAsync();

        /// <summary>
        /// Hàm rollback sau khi truy vấn thất bại
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        void RollBack();

        /// <summary>
        /// Hàm bất đồng bộ rollback sau khi truy vấn thất bại
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        Task RollBackAsync();
    }
}
