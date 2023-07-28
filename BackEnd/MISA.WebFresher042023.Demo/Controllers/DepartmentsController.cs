﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Application;
using MySqlConnector;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Api Department
    //  Author: HMDUC (14/06/2023)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<DepartmentDto,DepartmentInsertDto,DepartmentUpdateDto>
    {
        #region Field
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructor
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentService = departmentService;
        }

        #endregion

    }
}
