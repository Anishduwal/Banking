using Banking.Models;
using Banking.Repo.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Banking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IAccountRepo _accountRepo;

        public HomeController(ILogger<HomeController> logger, ITransactionRepo dbcontext, IAccountRepo accountRepo)
        {
            _logger = logger;
            _transactionRepo = dbcontext;
            _accountRepo = accountRepo;
        }

        public IActionResult Index()
        {
            List<AccountDetailsModel> data = _accountRepo.GetAccountDetails();
            //var data = _accountRepo.FindAccount(Convert.ToInt32(Id));

            return View(data);
        }
        public IActionResult SearchAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SearchAccount(string AccountNumber)
        {
            AccountDetailsModel data = _accountRepo.GetAccountInformation(AccountNumber);
            if (data == null)
            {
                ViewBag.Error = "No results found";
                return View();
            }
            return RedirectToAction("SearchResult", data);
        }
        public IActionResult SearchResult(AccountDetailsModel data)
        {
            return View(data);
        }

        public IActionResult Deposit([FromBody] TransactionDetailsModel model)
        {
            AccountDetailsModel data = _accountRepo.GetAccountInformation(model.AccountNumber);
            model.TransactionDate = DateTime.Now;
            model.TransactionAmount = model.amount;
            model.TransactionType = "Debit";
            TransactionDetailsModel result = _transactionRepo.AddTransaction(model);
            return RedirectToAction("SearchAccount");
        }
        public IActionResult Withdraw([FromBody] TransactionDetailsModel model)
        {
            AccountDetailsModel data = _accountRepo.GetAccountInformation(model.AccountNumber);
            model.TransactionDate = DateTime.Now;
            model.TransactionAmount = model.amount;
            model.TransactionType = "Credit";
            TransactionDetailsModel result = _transactionRepo.AddTransaction(model);

            //if (model.TransactionAmount > data.Amount)
            //{
            //    ViewBag.Error = "Cannot send more amount than actual amount";
            //}
            //else
            //{
            //    data.Amount -= model.TransactionAmount;
            //}
            return RedirectToAction("SearchAccount");
        }
        public IActionResult AddAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAccount(AccountDetailsModel model)
        {
            model.CreatedDate = DateTime.Now;
            model.IsActive = true;
            var result = _accountRepo.CreateAccount(model);
            List<AccountDetailsModel> data = _accountRepo.GetAccountDetails();
            return View("Index", data);
        }
        public JsonResult CheckBalance([FromBody] TransactionDetailsModel model)
        {
            var result = _accountRepo.CheckBalance(model);
            return Json(result);
        }
    }
}