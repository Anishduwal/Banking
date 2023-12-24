using Banking.Models;
using Banking.Data;
using Dapper;
using System.Data;

namespace Banking.Repo.Account
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly DapperContext _context;
        public TransactionRepo(DapperContext context)
        {
            _context = context;
        }
        public List<TransactionDetailsModel> ReadTransactionDetails()
        {
            List<TransactionDetailsModel> obj = new List<TransactionDetailsModel>();
            var param = new DynamicParameters();
            //param.Add("@Flag", "Select");
            using (var connection = _context.CreateConnection())
            {
                obj = connection.Query<TransactionDetailsModel>(sql: "[dbo].[sproc_Read_organization]", param: param, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public TransactionDetailsModel AddTransaction(TransactionDetailsModel model)
        {
            TransactionDetailsModel obj = new TransactionDetailsModel();
            var param = new DynamicParameters();
            param.Add("@Flag", "Insert");
            param.Add("@AccountId", model.AccountId);
            param.Add("@TransactionType", model.TransactionType);
            param.Add("@AccountNumber", model.AccountNumber);
            param.Add("@TransactionAmount", model.TransactionAmount);
            param.Add("@TransactionDate", model.TransactionDate);
            param.Add("@Remarks", model.Remarks);

            using (var connection = _context.CreateConnection())
            {
                obj = connection.QueryFirstOrDefault<TransactionDetailsModel>(sql: "[dbo].[sproc_add_transactions]", param: param, commandType: CommandType.StoredProcedure);
                return new TransactionDetailsModel
                {
                    Id = obj.Id
                };
            }
        }
        
    }
    
}
