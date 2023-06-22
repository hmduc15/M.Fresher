using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.MISAException;
using System.Windows.Markup;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public abstract class BaseService<TEntity, TEnityDto, TEntityInsertDto, TEntityUpdateDto> : IBaseService<TEnityDto, TEntityInsertDto, TEntityUpdateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        /// <summary>
        ///  Function Get list Record
        /// </summary>
        /// <returns>List Asset</returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<List<TEnityDto>> GetAllAsync()
        {
            var entityList = await _baseRepository.GetAllAsync();
            var entityDtos = new List<TEnityDto>();

            foreach (var entity in entityList)
            {
                var entityDto = _mapper.Map<TEnityDto>(entity);
                entityDtos.Add(entityDto);
            }

            return entityDtos;

        }

        /// <summary>
        /// Function  Get Record by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Entity</returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<TEnityDto> GetByIdAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(Resources.ResourceVN.Error_NotExit_Asset, 404);
            }

            var entityDto = _mapper.Map<TEnityDto>(entity);

            return entityDto;

        }

        /// <summary>
        ///  Functio Get  by Code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<TEnityDto> GetByCodeAsync(string code)
        {
            var entity = await _baseRepository.GetByCodeAsync(code);


            var entityDto = _mapper.Map<TEnityDto>(entity);

            return entityDto;

        }

        /// <summary>
        /// Function Insert a record 
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>
        /// Row Affected
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<int> InsertAsync(TEntityInsertDto entityInsertDto)
        {
            //Get table Name   
            var tableName = typeof(TEntityInsertDto).Name.Replace("InsertDto", "");

            // Get column EntityCode
            var columnCode = $"{tableName}Code";
            var columnName = $"{tableName}Name";


            //Get EntityInsertDto Code of param entityInsertDto
            var entityCode = entityInsertDto?.GetType().GetProperty(columnCode)?.GetValue(entityInsertDto)?.ToString();
            var entityName = entityInsertDto.GetType().GetProperty(columnName)?.GetValue(entityInsertDto)?.ToString();

            var entityByCode = await this.GetByCodeAsync(entityCode);

            var errorData = new Dictionary<string?, string>();


            //check validate data
            this.CheckNull(errorData, entityCode, columnCode, Resources.ResourceVN.Empty_Code);
            this.CheckNull(errorData, entityName, columnName, Resources.ResourceVN.Empty_Name);
            this.CheckLength(errorData, entityCode, columnCode, Resources.ResourceVN.Error_Length_Code);

            //check validate busines
            this.CheckValidateBusiness(errorData, entityInsertDto);

           


            //check duplicate entitycode
            if (entityByCode != null)
            {
                errorData.Add(columnCode, Resources.ResourceVN.Error_Dupli_Code);
            }

            if (errorData.Count > 0)
            {
                throw new ValidateException(errorData, (int)MISACode.Validate);
            }

            var entity = _mapper.Map<TEntity>(entityInsertDto);
            var columnId = $"{tableName}Id";

            //Initial GuiId for Column Entity Id 
            var entityId = typeof(TEntity).GetProperty($"{columnId}");
            if (entityId != null)
            {
                entityId.SetValue(entity, Guid.NewGuid());
            }

            var rowEffected = await _baseRepository.InsertAsync(entity);

            return rowEffected;
        }

        /// <summary>
        /// Function Update a record 
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>
        /// Row Affected
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<int> UpdateAsync(TEntityUpdateDto entityUpdateDto)
        {
            //Get table Name   
            var tableName = typeof(TEntityUpdateDto).Name.Replace("UpdateDto", "");

            // Get column Entity
            var columnCode = $"{tableName}Code";
            var columnName = $"{tableName}Name";

            //Get EntityInsertDto Field of param entityInsertDto
            var entityCode = entityUpdateDto?.GetType().GetProperty(columnCode)?.GetValue(entityUpdateDto)?.ToString();
            var entityName = entityUpdateDto.GetType().GetProperty(columnName)?.GetValue(entityUpdateDto)?.ToString();

            var entityByCode = await this.GetByCodeAsync(entityCode);
            var errorData = new Dictionary<string?, string>();

            //check validate data
            this.CheckNull(errorData, entityCode, columnCode, Resources.ResourceVN.Empty_Code);
            this.CheckNull(errorData, entityName, columnName, Resources.ResourceVN.Empty_Name);
            this.CheckLength(errorData, entityCode, columnCode, Resources.ResourceVN.Error_Length_Code);

            

            //check Duplicate EntityCode
            if (entityByCode != null)
           {
                errorData.Add(columnCode, Resources.ResourceVN.Error_Dupli_Code);
            }


            if (errorData.Count > 0)
            {
                throw new ValidateException(errorData, 400);
            }


            var entity = _mapper.Map<TEntity>(entityUpdateDto);

            var rowEffected = await _baseRepository.UpdateAsync(entity);

            return rowEffected;
        }

        /// <summary>
        /// Function delete entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        ///  Row Affected
        /// </returns>
        /// Author: HMDUC (19/06/2023)
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

        /// <summary>
        /// Function Delete Mul Async
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>
        /// RowsAffected
        /// </returns>
        /// Author: HMDUC (20/06/2023)
        public async Task<int> DeleteMulAsync(List<Guid> ids)
        {
            var rowsAffected = await _baseRepository.DeleteEntityMulAsync(ids);

            return rowsAffected;
        }


        /// <summary>
        /// Funtion check null Entity Field
        /// </summary>
        /// <param name="errData"></param>
        /// <param name="value"></param>
        /// <param name="entityField"></param>
        /// <param name="errMessage"></param>
        /// Author: HMDUC (22/06/2023)
        private void CheckNull(Dictionary<string, string> errData, string? value, string? entityField, string? errMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                errData.Add(entityField, errMessage);
            }
        }

        /// <summary>
        /// Function check Length Entity Field
        /// </summary>
        /// <param name="errData"></param>
        /// <param name="value"></param>
        /// <param name="entityField"></param>
        /// <param name="errorMessage"></param>
        /// Author: HMDUC (22/06/2023)
        private void CheckLength(Dictionary<string, string> errData, string? value, string? entityField, string? errorMessage)
        {
            if (value?.Length > 20)
            {
                errData.Add(entityField, errorMessage);
            }
        }

        /// <summary>
        /// Function check validate business
        /// </summary>
        /// <param name="errData"></param>
        /// <param name="entityInsertDto"></param>
        /// <param name="errorMessage"></param>
        /// Author: HMDUC (22/06/2023)
        private void CheckValidateBusiness(Dictionary<string,string> errData,TEntityInsertDto entityInsertDto)
        {
            var purchaseDate = entityInsertDto.GetType().GetProperty("PurchaseDate").GetValue(entityInsertDto).ToString();
            var productionYear = entityInsertDto.GetType().GetProperty("ProductionYear").GetValue(entityInsertDto).ToString();
            
            if(DateTime.Parse(purchaseDate) > DateTime.Now)
            {
                errData.Add("PurchaseDate",Resources.ResourceVN.Error_PurchaseDate );
            }
            if(DateTime.Parse(productionYear) > DateTime.Now)
            {
                errData.Add("ProductionYear", Resources.ResourceVN.Error_ProductionYear);
            }
            if(DateTime.Parse(purchaseDate) > DateTime.Parse(productionYear))
            {
                errData.Add("ProductionAndPurchaseDate",Resources.ResourceVN.Error_PurchaseDateAndProductionYear);
            }


        }

    }
}
