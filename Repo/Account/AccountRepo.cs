using Banking.Models;
using Banking.Data;
using Dapper;
using System.Data;
using Microsoft.VisualBasic;

namespace Banking.Repo.Account
{
    public class AccountRepo : IAccountRepo
    {
        private readonly DapperContext _context;
        public AccountRepo(DapperContext context)
        {
            _context = context;
        }
        public UserDetailsModel CreateAdmin(UserDetailsModel model)
        {
            UserDetailsModel obj = new UserDetailsModel();
            var param = new DynamicParameters();
            param.Add("@Flag", "Insert");
            param.Add("@Id", model.Id);
            param.Add("@Username", model.Username);
            param.Add("@Password", model.Password);
            using (var connection = _context.CreateConnection())
            {
                obj = connection.QueryFirstOrDefault<UserDetailsModel>(sql: "[dbo].[sproc_create_or_update_account]", param: param, commandType: CommandType.StoredProcedure);
                return new UserDetailsModel
                {
                    Id = obj.Id
                };
            }
        }
        public UserDetailsModel Login(string Username, string Password)
        {
            UserDetailsModel obj = new UserDetailsModel();
            var param = new DynamicParameters();
            param.Add("@UserName", Username);
            param.Add("@Password", Password);
            using (var connection = _context.CreateConnection())
            {
                obj = connection.QueryFirstOrDefault<UserDetailsModel>(sql: "[dbo].[sproc_check_username_and_password]", param: param, commandType: CommandType.StoredProcedure);
                return new UserDetailsModel
                {
                    Id = obj.Id,
                    Code = obj.Code,
                    Message = obj.Message
                };
            };
        }
        public AccountDetailsModel FindAccount(int Id)
        {
            AccountDetailsModel obj = new AccountDetailsModel();
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            using (var connection = _context.CreateConnection())
            {
                obj = connection.QueryFirstOrDefault<AccountDetailsModel>(sql: "[dbo].[sproc_get_account]", param: param, commandType: CommandType.StoredProcedure);
                return new AccountDetailsModel
                {
                    Id = obj.Id,
                    AccountHolder = obj.AccountHolder,
                    AccountNumber = obj.AccountNumber                    
                };
            }
        }
        public List<AccountDetailsModel> GetAccountDetails()
        {
            List<AccountDetailsModel> obj = new List<AccountDetailsModel>();
            var param = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<AccountDetailsModel>(
                                sql: "[dbo].[sproc_get_account_details]",
                                param: param,
                                commandType: CommandType.StoredProcedure 
                            ).ToList();
                return result;
            }
        }

        public AccountDetailsModel GetAccountInformation(string AccountNumber)
        {
            AccountDetailsModel obj = new AccountDetailsModel();
            var param = new DynamicParameters();
            param.Add("@AccountNumber", AccountNumber);
            using (var connection = _context.CreateConnection())
            {
                var result = connection.QueryFirstOrDefault<AccountDetailsModel>(
                                sql: "[dbo].[sproc_get_account_information]",
                                param: param,
                                commandType: CommandType.StoredProcedure
                            );
                return result;
            }
        }
        public AccountDetailsModel CreateAccount(AccountDetailsModel model)
        {
            AccountDetailsModel obj = new AccountDetailsModel();
            var param = new DynamicParameters();
            param.Add("@Flag", "Insert");
            param.Add("@Id", model.Id);
            param.Add("@AccountHolder", model.AccountHolder);
            param.Add("@AccountNumber", model.AccountNumber);
            param.Add("@Email", model.Email);
            param.Add("@PhoneNumber", model.PhoneNumber);
            param.Add("@CreatedDate", model.CreatedDate);
            param.Add("@IsActive", model.IsActive);
            using (var connection = _context.CreateConnection())
            {
                obj = connection.QueryFirstOrDefault<AccountDetailsModel>(sql: "[dbo].[sproc_add_account]", param: param, commandType: CommandType.StoredProcedure);
                return new AccountDetailsModel
                {
                    Id = obj.Id
                };
            }
        }

    }

}
