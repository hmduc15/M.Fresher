using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.DepartmentDto;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
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
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper) 
        {
            _departmentRepository = departmentRepository;
            
        }


    }
}
