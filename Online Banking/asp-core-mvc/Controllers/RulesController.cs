using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using asp_core_mvc.Models;
using asp_core_mvc.ViewModels;
using Microsoft.AspNetCore.Http;

namespace asp_core_mvc.Controllers
{
    public class RulesController : Controller
    {
        private RulesModel GenerateRulesModel(int account = 0)
        {
            RulesModel rulesModel = new RulesModel();
            Int32 customerID = (Int32)HttpContext.Session.GetInt32("CustomerID");
            List<Int32> accountIDs = DatabaseHandler.getAccounts(customerID);
            rulesModel.accounts = accountIDs;
            if (accountIDs.Count > 0)
            {
                if(account > 0)
                {
                    rulesModel.curAccount = account;
                    rulesModel.rules = DatabaseHandler.getRules(customerID, account);
                }
                else
                {
                    rulesModel.curAccount = accountIDs[0];
                    rulesModel.rules = DatabaseHandler.getRules(customerID, accountIDs[0]);
                }
            }
            return rulesModel;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Get("CustomerID") == null)
                return RedirectToAction("Index", "Login");

            return View(GenerateRulesModel());
        }

        [HttpPost]
        public IActionResult Index(RulesModel ruleModel)
        {
            if (HttpContext.Session.Get("CustomerID") == null)
                return RedirectToAction("Index", "Login");

            return View(GenerateRulesModel(ruleModel.curAccount));
        }

        [HttpPost]
        public IActionResult Edit(RulesModel ruleModel)
        {
            if (HttpContext.Session.Get("CustomerID") == null)
                return RedirectToAction("Index", "Login");

            if(ruleModel.rules.startTrans < new DateTime(1900, 1, 1) ||
                ruleModel.rules.startTrans > new DateTime(9999, 12, 31))
            {
                ModelState.AddModelError("startTrans", "The field Start date must be between 1/1/1900 and 12/31/9999.");
            }
            if (ruleModel.rules.endTrans < new DateTime(1900, 1, 1) ||
                ruleModel.rules.endTrans > new DateTime(9999, 12, 31))
            {
                ModelState.AddModelError("endTrans", "The field End date must be between 1/1/1900 and 12/31/9999.");
            }

            if(ruleModel.rules.startTrans > ruleModel.rules.endTrans)
            {
                ModelState.AddModelError("startTrans", "The Start date must be before End date.");
            }

            Int32 customerID = (Int32)HttpContext.Session.GetInt32("CustomerID");
            if (!ModelState.IsValid)
            {
                ruleModel.curAccount = ruleModel.rules.accountID;
                ruleModel.accounts = DatabaseHandler.getAccounts(customerID);
                return View("Index", ruleModel);
            }

            if (ruleModel.rules.OutStateTrans == false && ruleModel.rules.rangeTrans == false &&
                ruleModel.rules.catTrans == false && ruleModel.rules.greatTrans == false &&
                ruleModel.rules.greatDepo == false && ruleModel.rules.greatWithdraw == false &&
                ruleModel.rules.greatBal == false && ruleModel.rules.lessBal == false)
                DatabaseHandler.deleteRules(customerID, ruleModel.rules.accountID);
            else
                DatabaseHandler.setRules(customerID, ruleModel.rules);

            ViewBag.Success = true;

            return View("Index", GenerateRulesModel(ruleModel.rules.accountID));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
