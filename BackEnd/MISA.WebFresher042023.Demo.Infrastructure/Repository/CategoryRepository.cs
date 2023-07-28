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
    /// Implement Interface Category Repository
    /// </summary>
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }
    }
}
