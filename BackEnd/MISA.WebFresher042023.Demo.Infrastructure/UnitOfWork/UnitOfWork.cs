using MISA.WebFresher042023.Demo.Domain;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.UnitOfWork
{
    /// <summary>
    /// UnitOfWork quản lý transaction
    /// </summary>
    /// Author: HMDUC (24/07/2023)
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region Field
        private readonly DbConnection _connection;
        private DbTransaction? _transaction = null;
        #endregion

        #region Constructor
        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }
        #endregion


        #region Method

        /// <summary>
        ///  Hàm connect Db
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public DbConnection Connection => _connection;

        /// <summary>
        /// Hàm transaction
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public DbTransaction? Transaction => _transaction;

        /// <summary>
        /// Hàm mở transaction
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public void BeginTransaction()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = _connection.BeginTransaction();
            }
            else
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
        }

        /// <summary>
        /// Hàm bất đồng bộ mở transaction
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public async Task BeginTrasactionAsync()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
            else
            {
                await _connection.OpenAsync();
                _transaction = await _connection.BeginTransactionAsync();
            }
        }

        /// <summary>
        /// Hàm commit sau khi truy vấn thành công
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public void Commit()
        {
            _transaction?.Commit();
            Dispose(); ;
        }

        /// <summary>
        /// Hàm bất đồng bộ sau khi truy vấn thành công 
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            await DisposeAsync();
        }

        /// <summary>
        /// Hàm giải phóng tài nguyên sau khi truy vấn  
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            _connection.Close();
        }

        /// <summary>
        /// Hàm bất đồng bộ giải phóng tài nguyên sau khi truy vấn 
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            _transaction = null;
            await _connection.CloseAsync();
        }

        /// <summary>
        /// Hàm rollback nếu truy vấn thất bại
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public void RollBack()
        {
            _transaction?.Rollback();
            Dispose();
        }

        /// <summary>
        /// Hàm bất đồng bộ rollback nếu truy vấn thất bại
        /// </summary>
        /// Author: HMDUC (24/07/2023)
        public async Task RollBackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            await DisposeAsync();

        } 
        #endregion
    }
}
