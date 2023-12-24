using Banking.Models;

namespace Banking.Repo.Account
{
    public interface ITransactionRepo
    {
        public List<TransactionDetailsModel> ReadTransactionDetails();
        public TransactionDetailsModel AddTransaction(TransactionDetailsModel model);


    }
}
