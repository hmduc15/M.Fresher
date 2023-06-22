namespace MISA.WebFresher042023.Demo.Core.Enum
{
    /// <summary>
    /// Status code request
    /// </summary>
    /// Author: HMDUC (13/06/2023)
    public enum MISACode : ushort
    {
        /// <summary>
        /// Sucsess
        /// </summary>
        Success = 200,
        
        /// <summary>
        /// created success
        /// </summary>
        Created = 201,

        /// <summary>
        /// Failed
        /// </summary>
        BadResquest = 400,

        /// <summary>
        /// Error validate
        /// </summary>
        Validate=400,

        /// <summary>
        /// Error Exception
        /// </summary>
        Exception = 500,    

        /// <summary>
        /// Not found data
        /// </summary>
        NotFound = 404,


        /// <summary>
        /// Duplicate data
        /// </summary>
        Duplicate = 409,

    }
}
