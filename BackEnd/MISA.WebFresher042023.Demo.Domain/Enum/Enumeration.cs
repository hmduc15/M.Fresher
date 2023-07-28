namespace MISA.WebFresher042023.Demo.Domain.Enum
{
    /// <summary>
    /// Enum của project
    /// </summary>
    /// Author: HMDUC (13/06/2023)
    public enum MISACode : ushort
    {
        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200,
        
        /// <summary>
        /// Thêm mới thành công
        /// </summary>
        Created = 201,

        /// <summary>
        /// Request thất bại
        /// </summary>
        BadResquest = 400,

        /// <summary>
        /// Lỗi validate
        /// </summary>
        Validate=400,

        /// <summary>
        /// Lỗi Exception
        /// </summary>
        Exception = 500,    

        /// <summary>
        /// Lỗi không tìm thấy
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// Lỗi trùng dữ liệu
        /// </summary>
        Duplicate = 409,

    }
}
