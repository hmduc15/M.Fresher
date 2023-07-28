using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Domain;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Field
        protected readonly IUnitOfWork _uow;
        public virtual string TableName { get; } = typeof(TEntity).Name;

        #endregion

        #region Constructor
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        #endregion

        /// <summary>
        ///  Hàm lấy ra danh sách Entity từ DB
        /// </summary>
        /// <returns>Danh sách Entity</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetAllAsync
        public async Task<List<TEntity>> GetAllAsync()
        {

            //connect mysql
            var sqlCommand = $"CALL Proc_{TableName}_GetAll";

            var assets = await _uow.Connection.QueryAsync<TEntity>(sql: sqlCommand, transaction: _uow.Transaction);

            return assets.ToList();

        }
        #endregion

        /// <summary>
        ///  Hàm lấy Entity theo mã từ DB
        /// </summary>
        /// <param name="code">Mã Entity</param>
        /// <returns>Entity</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetByCodeAsync
        public async Task<TEntity> GetByCodeAsync(string code)
        {
            //Get TableName Entity
            var TableName = typeof(TEntity).Name;

            //Sql command line
            var sqlCommand = $"SELECT * FROM {TableName} WHERE {TableName}Code = @code";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var entity = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql: sqlCommand, param: parameters, transaction: _uow.Transaction);

            return entity;
        }
        #endregion

        /// <summary>
        /// Hàm lấy Entity theo Id
        /// </summary>
        /// <param name="Id">Id của Entity</param>
        /// <returns>Entity</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetByIdAsync
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            //Sql command line
            var sqlCommand = $"SELECT * FROM {TableName} WHERE {TableName}Id = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var entity = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql: sqlCommand, param: parameters, transaction: _uow.Transaction);

            return entity;
        }
        #endregion


        /// <summary>
        /// Hàm thêm mới Entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>
        /// Row Affected: Số dòng bị ảnh hưởng 
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region InsertAsync
        public async Task<int> InsertAsync(TEntity entity)
        {
            int rowAffected = 0;
            try
            {
                await _uow.BeginTrasactionAsync();

                //Sql command line
                var sqlCommandProc = $"Proc_{TableName}_Insert";


                var sqlCommand = (MySqlCommand)_uow.Connection.CreateCommand();
                sqlCommand.CommandText = sqlCommandProc;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlCommandBuilder.DeriveParameters(sqlCommand);
                DynamicParameters? parameters = new DynamicParameters();

                foreach (MySqlParameter param in sqlCommand.Parameters)
                {
                    var paramKey = param.ParameterName;
                    var propValue = paramKey.Replace("@m_", "");
                    var paramValue = entity.GetType().GetProperty(propValue)?.GetValue(entity);

                    parameters.Add(paramKey, paramValue);

                }
                rowAffected = await _uow.Connection.ExecuteAsync(sql: sqlCommandProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);

                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await _uow.RollBackAsync();
            }


            return rowAffected;
        }
        #endregion

        /// <summary>
        /// Hàm cập nhật Entity
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>
        ///  Rows effected: Số dòng bị ảnh hưởng
        /// </returns>
        ///  Author: HMDUC (19/06/2023)
        #region UpdateAsync
        public async Task<int> UpdateAsync(TEntity entity)
        {
            int rowAffected = 0;
            try
            {
                await _uow.BeginTrasactionAsync();

                //Sql command line
                var sqlCommandProc = $"Proc_{TableName}_Update";

                var sqlCommand = (MySqlCommand)_uow.Connection.CreateCommand();
                sqlCommand.CommandText = sqlCommandProc;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlCommandBuilder.DeriveParameters(sqlCommand);
                DynamicParameters? parameters = new DynamicParameters();

                foreach (MySqlParameter param in sqlCommand.Parameters)
                {
                    var paramKey = param.ParameterName;
                    var propValue = paramKey.Replace("@M_", "");
                    var paramValue = entity?.GetType().GetProperty(propValue)?.GetValue(entity);

                    parameters.Add(paramKey, paramValue);
                }

                rowAffected = await _uow.Connection.ExecuteAsync(sql: sqlCommandProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);

                await _uow.CommitAsync();
            }catch(Exception ex)
            {
                await _uow.RollBackAsync();
            }
            
            return rowAffected;
        }
        #endregion



        /// <summary>
        /// Hàm xóa Entity theo Id
        /// </summary>
        /// <param name="Id">ID của Entity</param>
        /// <returns>
        ///  rowsAffected: Số dòng bị ảnh hưởng
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region DeleteEnityAsync
        public async Task<int> DeleteEntityAsync(Guid id)
        {
            //Sql command line
            var sqlCommand = $"DELETE FROM {TableName} WHERE {TableName}Id = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            int rowsAffected = await _uow.Connection.ExecuteAsync(sql: sqlCommand, param: parameters, transaction: _uow.Transaction);

            return rowsAffected;
        }
        #endregion


        /// <summary>
        /// Hàm xóa nhiều theo danh sách Id
        /// </summary>
        /// <param name="ids">Danh sách  Id</param>
        /// <returns>
        /// rowsAffected: Số dòng bị ảnh hưởng
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region DeleteMultipleAsync
        public async Task<int> DeleteEntityMulAsync(List<Guid> ids)
        {
            int rowsAffected =0;
            try
            {
                await _uow.BeginTrasactionAsync();

                //Sql command line
                var sqlCommand = $"Proc_{TableName}_DeleteMulti";

                DynamicParameters parameters = new DynamicParameters();
                var paramString = string.Join(",", ids);
                parameters.Add("@ids", paramString);

                rowsAffected = await _uow.Connection.ExecuteAsync(sql: sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);

               await _uow.CommitAsync();
            }catch(Exception ex)
            {
                await _uow.RollBackAsync();
            }

            return rowsAffected;
        }
        #endregion
    }
}
