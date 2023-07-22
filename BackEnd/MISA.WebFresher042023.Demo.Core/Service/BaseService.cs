using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.MISAException;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityInsertDto, TEntityUpdateDto> : IBaseService<TEntityDto, TEntityInsertDto, TEntityUpdateDto>
    {
        #region Field
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructor
        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }
        #endregion

        /// <summary>
        ///  Hàm lấy ra danh sách EntityDto từ Db
        /// </summary>
        /// <returns>List EntityDto</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetAllAsync
        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var entityList = await _baseRepository.GetAllAsync();
            var entityDtos = new List<TEntityDto>();

            //if(entitylist == null)
            //{
            //    return null;
            //}

            foreach (var entity in entityList)
            {
                var entityDto = _mapper.Map<TEntityDto>(entity);
                entityDtos.Add(entityDto);
            }


            return entityDtos;

        }
        #endregion

        /// <summary>
        /// Hàm lấy EntityDto theo Id
        /// </summary>
        /// <param name="Id">ID entity</param>
        /// <returns>entityDto</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetByIdAsync
        public async Task<TEntityDto> GetByIdAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(Resources.ResourceVN.Error_NotExit_Asset, 404);
            }

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;

        }
        #endregion

        /// <summary>
        ///  Hàm lấy EntityDto theo Code
        /// </summary>
        /// <param name="Code">Mã Entity</param>
        /// <returns>entityDto</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetByCodeAsync
        public async Task<TEntityDto> GetByCodeAsync(string code)
        {
            var entity = await _baseRepository.GetByCodeAsync(code);


            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;

        }
        #endregion

        /// <summary>
        /// Hàm thêm mới EntityInsertDto
        /// </summary>
        /// <param name="entityInsertDto">TEntityInsertDto</param>
        /// <returns>
        /// Row Affected: Số dòng bị ảnh hưởng
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region InsertAsync
        public async Task<int> InsertAsync(TEntityInsertDto entityInsertDto)
        {
            var errorData = new Dictionary<string, string>();
            var entity = _mapper.Map<TEntity>(entityInsertDto);

            //Get table Name   
            var tableName = typeof(TEntityInsertDto).Name.Replace("InsertDto", "");

            // Check validate data
            ValidateEntityData(entity);

            //Check duplicate
            var columnCode = $"{tableName}Code";
            var entityCode = entity?.GetType().GetProperty(columnCode)?.GetValue(entity)?.ToString();
            CheckDuplicate(entity, entityCode);

            //Check Validate Business
            this.CheckValidateBusiness(entity);

            //Initial Data for Entity
            var columnId = $"{tableName}Id";

            var entityId = typeof(TEntity).GetProperty($"{columnId}");
            var createdDate = typeof(TEntity).GetProperty("CreatedDate");

            if (entityId != null && createdDate != null)
            {
                entityId.SetValue(entity, Guid.NewGuid());
                createdDate.SetValue(entity, DateTime.Now);
            }

            var rowEffected = await _baseRepository.InsertAsync(entity);

            return rowEffected;
        }
        #endregion

        /// <summary>
        /// Hàm cập nhật EntityInsertDto
        /// </summary>
        /// <param name="entityUpdateDto">TEntityUpdateDto</param>
        /// <returns>
        /// Row Affected: Số dòng bị ảnh hưởng
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region UpdateAsync
        public async Task<int> UpdateAsync(TEntityUpdateDto entityUpdateDto)
        {
            var errorData = new Dictionary<string, string>();
            var entity = _mapper.Map<TEntity>(entityUpdateDto);

            //Get table Name   
            var tableName = typeof(TEntityUpdateDto).Name.Replace("UpdateDto", "");

            // Check validate data
            ValidateEntityData(entity);

            //Check duplicate
            var columnCode = $"{tableName}Code";
            var entityCode = entity?.GetType().GetProperty(columnCode)?.GetValue(entity)?.ToString();
            CheckDuplicate(entity, entityCode, false);


            //Initial data ModifiedDate
            var modifiedDate = typeof(TEntity).GetProperty("ModifiedDate");
            if (modifiedDate != null)
            {
                modifiedDate.SetValue(entity, DateTime.Now);
            }

            var rowEffected = await _baseRepository.UpdateAsync(entity);

            return rowEffected;
        }
        #endregion

        /// <summary>
        /// Hàm xóa Entity theo Id
        /// </summary>
        /// <param name="id">Id Entity</param>
        /// <returns>
        ///  Row Affected: Số dòng bị ảnh hưởng
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region DeleteAsync
        public async Task<int> DeleteAsync(Guid id)
        {
            var res = await _baseRepository.GetByIdAsync(id);

            //check exit Asset before delete
            if (res == null)
            {
                throw new NotFoundException(Resources.ResourceVN.Error_NotExit_Asset, 404);
            }

            var rowAffected = await _baseRepository.DeleteEntityAsync(id);

            return rowAffected;
        }
        #endregion

        /// <summary>
        /// Hàm xóa nhiều theo danh sách Id
        /// </summary>
        /// <param name="ids">Danh sách Id</param>
        /// <returns>
        /// RowsAffected: Số dòng bị ảnh hưởng
        /// </returns>
        /// Author: HMDUC (20/06/2023)
        #region DeleteMulAsync
        public async Task<int> DeleteMulAsync(List<Guid> ids)
        {
            var rowsAffected = await _baseRepository.DeleteEntityMulAsync(ids);

            return rowsAffected;
        }
        #endregion

        /// <summary>
        /// Hàm Validate data
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <exception cref="ValidateException"></exception>
        /// Author: HMDUC (20/06/2023)
        #region ValidaeEnityData
        private void ValidateEntityData(TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var columnCode = $"{tableName}Code";
            var columnName = $"{tableName}Name";

            var entityCode = entity?.GetType().GetProperty(columnCode)?.GetValue(entity)?.ToString();
            var entityName = entity?.GetType().GetProperty(columnName)?.GetValue(entity)?.ToString();

            var errorData = new Dictionary<string, string>();

            CheckNull(errorData, entityCode, columnCode, Resources.ResourceVN.Empty_Code);
            CheckNull(errorData, entityName, columnName, Resources.ResourceVN.Empty_Name);
            CheckLength(errorData, entityCode, columnCode, Resources.ResourceVN.Error_Length_Code, 20);

            if (errorData.Count > 0)
            {
                throw new ValidateException(errorData, (int)MISACode.Validate);
            }
        } 
        #endregion

        /// <summary>
        /// Hàm check trùng mã tài sản 
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <param name="code">Mã Entity</param>
        /// <param name="isInsert">Check Insert or Update</param>
        /// Author: HMDUC (22/06/2023)
        protected virtual void CheckDuplicate(TEntity entity, string? code, bool isInsert = true)
        {
        }

        /// <summary>
        /// Hàm check null Entity Field 
        /// </summary>
        /// <param name="errData">List Danh sách lỗi</param>
        /// <param name="value">Giá trị cần check</param>
        /// <param name="entityField">Tên Entity Field cần check</param>
        /// <param name="errMessage">Message lỗi</param>
        /// Author: HMDUC (22/06/2023)
        protected virtual void CheckNull(Dictionary<string, string> errData, string? value, string? entityField, string errMessage)
        {

        }

        /// <summary>
        ///  Hàm check length Entity Field
        /// </summary>
        /// <param name="errData">List Danh sách lỗi</param>
        /// <param name="value">Giá trị cần check</param>
        /// <param name="entityField">Tên Entity Field cần check</param>
        /// <param name="errMessage">Message lỗi</param>
        /// Author: HMDUC (22/06/2023)
        protected virtual void CheckLength(Dictionary<string, string> errData, string? value, string entityField, string errorMessage, int maxLength)
        {

        }

        /// <summary>
        /// Hàm check validate nghiệp vụ
        /// </summary>
        /// <param name="entity">Entity cần check</param>
        /// Author: HMDUC (22/06/2023)
        protected virtual void CheckValidateBusiness(TEntity entity)
        {
        }
    }
}
