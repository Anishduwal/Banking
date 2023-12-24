using Banking.Models;

namespace Banking.Repo.Account
{
    public interface IAccountRepo
    {
        public UserDetailsModel CreateAccount(UserDetailsModel model);
        public UserDetailsModel Login(string Username, string Password);
        public AccountDetailsModel FindAccount(int Id);
        public List<AccountDetailsModel> GetAccountDetails();
        public AccountDetailsModel GetAccountInformation(string AccountNumber);

    }
}
