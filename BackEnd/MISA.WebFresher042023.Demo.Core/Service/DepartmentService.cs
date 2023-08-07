using AutoMapper;
using MISA.WebFresher042023.Demo.Application.Dto.DepartmentDto;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Application.Service;
using MISA.WebFresher042023.Demo.Domain;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    /// <summary>
    /// Implement Department Interface
    /// </summary>
    public class DepartmentService : BaseService<Department,DepartmentDto, DepartmentInsertDto, DepartmentUpdateDto,DepartmentTranferDto>, IDepartmentService
    {
        #region Field
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(departmentRepository, mapper, unitOfWork)
        {
            _departmentRepository = departmentRepository;

        } 
        #endregion


    }
}
