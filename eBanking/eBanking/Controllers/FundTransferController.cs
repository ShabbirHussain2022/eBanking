using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eBanking;
using eBanking.Models;

namespace eBanking.Controllers
{
    public class FundTransferController : Controller
    {
        private eBankingEntities db = new eBankingEntities();

        public ActionResult FundTransfer()
        {
            ViewBag.FromAccount = new SelectList(db.Accounts, "AccountID", "AccountName");
            ViewBag.ToAccount = new SelectList(db.Accounts, "AccountID", "AccountName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FundTransfer(eBanking.Models.FundTransfer eFund)
        {
            if (ModelState.IsValid)
            {
                eBanking.Models.Account objFromAccount = db.Accounts.Where(t => t.AccountID == eFund.FromAccount).FirstOrDefault();
                eBanking.Models.Account objToAccount = db.Accounts.Where(t => t.AccountID == eFund.ToAccount).FirstOrDefault();


                if (eFund.Amount > objFromAccount.Balance)
                {
                    ViewBag.FromAccount = new SelectList(db.Accounts, "AccountID", "AccountName");
                    ViewBag.ToAccount = new SelectList(db.Accounts, "AccountID", "AccountName");

                    ModelState.AddModelError("FromAccount", "Balance is insufficent. Your current balace is " + objFromAccount.Balance.ToString("N2")); ;
                }
                else
                {
                    eBanking.Models.Transaction objTransaction = new Transaction();

                    objTransaction.TransactionDate = System.DateTime.Now;
                    objTransaction.FromAccountID = eFund.FromAccount;
                    objTransaction.ToAccountID = eFund.ToAccount;
                    objTransaction.TransactionAmount = eFund.Amount;
                    objTransaction.FromBalance = objFromAccount.Balance - eFund.Amount;
                    objTransaction.ToBalance = objToAccount.Balance + eFund.Amount;

                    db.Transactions.Add(objTransaction);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }                
            }

            return View(eFund);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
