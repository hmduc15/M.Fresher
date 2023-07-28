using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application
{
    public class AssetUpdateDto
    {  

        /// <summary>
        /// Id tài sản
        /// </summary>
        public  Guid AssetId{ get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string? AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string? AssetName { get; set; }

  
        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        public Guid DepartmentId { get; set; }


        /// <summary>
        /// Tỷ lệ hao mòn 
        /// </summary>
        public float? DepreciationRate { get; set; }
        
        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        public Decimal DepreciationYear { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }


        /// <summary>
        /// Ngày mua tài sản
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal? Cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///  Năm theo dõi
        /// </summary>
        public int? TrackedYear { get; set; }

        /// <summary>
        /// Số năm  sử dụng
        /// </summary>
        public int? LifeTime { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        public DateTime? ProductionYear { get; set; }

        /// <summary>
        /// TThời gian cập nhật
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

    }
}
