using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using asp_core_mvc.Models;
using asp_core_mvc.ViewModels;
using Microsoft.AspNetCore.Http;

namespace asp_core_mvc.Controllers
{
    public class AlertsController : Controller
    {
        private AlertsModel generateAlertsModel(int account = 0)
        {
            AlertsModel alertsModel = new AlertsModel();
            Int32 customerID = (Int32)HttpContext.Session.GetInt32("CustomerID");
            List<Int32> accountIDs = DatabaseHandler.getAccounts(customerID);
            alertsModel.accounts = accountIDs;
            if (accountIDs.Count > 0)
            {
                if (account > 0)
                {
                    alertsModel.curAccount = account;
                    alertsModel.alerts = DatabaseHandler.getAlerts(customerID, account);
                }
                else
                {
                    alertsModel.curAccount = accountIDs[0];
                    alertsModel.alerts = DatabaseHandler.getAlerts(customerID, accountIDs[0]);
                }
            }
            return alertsModel;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Get("CustomerID") == null)
                return RedirectToAction("Index", "Login");

            return View(generateAlertsModel());
        }

        [HttpPost]
        public IActionResult Index(AlertsModel alertsModel)
        {
            if (HttpContext.Session.Get("CustomerID") == null)
                return RedirectToAction("Index", "Login");

            return View(generateAlertsModel(alertsModel.curAccount));
        }

        public IActionResult Remove(int ID)
        {
            DatabaseHandler.removeAlert(ID);

            if (HttpContext.Session.Get("CustomerID") == null)
                return RedirectToAction("Index", "Login");

            return RedirectToAction("Index", "Alerts", generateAlertsModel());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
