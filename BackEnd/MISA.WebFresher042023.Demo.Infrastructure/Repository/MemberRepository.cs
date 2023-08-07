using Dapper;
using MISA.WebFresher042023.Demo.Domain;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class MemberRepository : BaseRepository<MemberReceipt>, IMemberRepository
    {

        public MemberRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Hàm lấy ra ds ban giao nhận trước đó
        /// </summary>
        /// <returns>Danh sách ban giao nhận</returns>
        /// Author: HMDUC (05/08/2023)
        public async Task<List<MemberReceipt>> GetPrevMemberAsync()
        {
            var sqlCommand = "Proc_Member_GetOldMember";

            var result =await _uow.Connection.QueryAsync<MemberReceipt>(sqlCommand,commandType: System.Data.CommandType.StoredProcedure);

            return result.ToList();
        }

        /// <summary>
        /// Hàm lấy ra ds ban giao nhận theo Id của chừn từ
        /// </summary>
        /// <param name="receiptId">Id chứng từ</param>
        /// <returns>Danh sách ban giao nhận</returns>
        /// Author: HMDUC(06/08/2023)
        public async Task<List<MemberReceipt>> GetMemberByReceiptIdAsync(Guid receiptId)
        {
            var sqlCommand = "SELECT * FROM member m WHERE m.ReceiptId = @ReceiptId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ReceiptId", receiptId);

            var result = await _uow.Connection.QueryAsync<MemberReceipt>(sqlCommand, parameters, transaction: _uow.Transaction);
           
            return result.ToList();

        }
    }
}
