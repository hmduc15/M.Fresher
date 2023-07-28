using AutoMapper;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Application.Service;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Interface;
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
    public class DepartmentService : BaseService<Department,DepartmentDto, DepartmentInsertDto, DepartmentUpdateDto>, IDepartmentService
    {
        #region Field
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
            _departmentRepository = departmentRepository;

        } 
        #endregion


    }
}
