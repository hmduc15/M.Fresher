using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Entity
{
    public abstract class BaseEntity
    {

        /// <summary>
        /// Person created Asset
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Date created Asset
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Person modified Asset
        /// </summary>
        public string? ModifiedBY { get; set; }

        /// <summary>
        /// Date modified Asset
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

    }
}
