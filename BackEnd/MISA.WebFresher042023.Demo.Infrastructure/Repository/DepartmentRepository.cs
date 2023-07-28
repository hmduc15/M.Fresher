using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Domain;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    /// <summary>
    /// Implement Interface Department Repository
    /// </summary>
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
            
        }


    }
}
