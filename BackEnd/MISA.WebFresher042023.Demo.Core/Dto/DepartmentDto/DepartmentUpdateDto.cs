using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application.Dto.DepartmentDto
{
    public class DepartmentUpdateDto
    {
        /// <summary>
        /// Code Department
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Name Department
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Description 
        /// </summary>
        public string? Description { get; set; }
    }
}
