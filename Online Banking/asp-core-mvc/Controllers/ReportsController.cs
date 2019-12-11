using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using asp_core_mvc.Models;
using asp_core_mvc.ViewModels;
using System;
using OfficeOpenXml;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace asp_core_mvc.Controllers
{
    public class ReportsController : Controller
    {
        private ReportsModel generateReportsModel(int account = 0)
        {
            ReportsModel reportsModel = new ReportsModel();
            Int32 customerID = (Int32)HttpContext.Session.GetInt32("CustomerID");
            List<Int32> accountIDs = DatabaseHandler.getAccounts(customerID);
            reportsModel.accounts = accountIDs;
            if (accountIDs.Count > 0)
            {
                if (account > 0)
                {
                    reportsModel.curAccount = account;
                    reportsModel.Reports = DatabaseHandler.getReports(customerID, account);
                    reportsModel.PrevReports = DatabaseHandler.getPrevReports(customerID, account);
                }
                else
                {
                    reportsModel.curAccount = accountIDs[0];
                    reportsModel.Reports = DatabaseHandler.getReports(customerID, accountIDs[0]);
                    reportsModel.PrevReports = DatabaseHandler.getPrevReports(customerID, accountIDs[0]);
                }
            }

            return reportsModel;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Get("CustomerID") == null)
                return RedirectToAction("Index", "Login");

            return View(generateReportsModel());
        }

        [HttpPost]
        public IActionResult Index(ReportsModel reportsModel)
        {
            if (HttpContext.Session.Get("CustomerID") == null)
                return RedirectToAction("Index", "Login");

            return View(generateReportsModel(reportsModel.curAccount));
        }

        [HttpGet]
        public IActionResult Export(String sd, String ed, int accountID) // EPPlus [params = startdate, enddate]
        {
            byte[] result;

            ExcelPackage pkg = new ExcelPackage();
            ExcelWorksheet ws = pkg.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Online Banking Report";
            ws.Cells["A3"].Value = "Start Date:";
            ws.Cells["A4"].Value = "End Date:";
            ws.Cells["A5"].Value = "Alerts Tripped:";

            ws.Cells["A7"].Value = "Date";
            ws.Cells["B7"].Value = "Reason for Alert";
            ws.Cells["C7"].Value = "Description";
            ws.Cells["D7"].Value = "Location";
            ws.Cells["E7"].Value = "Amount";
            ws.Cells["F7"].Value = "Balance";
            //ws.Cells["G7"].Value = "TransID";

            Int32 customerID = (Int32)HttpContext.Session.GetInt32("CustomerID");
            List<Reports> reps = DatabaseHandler.getPrevReports(customerID, accountID);
            ws.Cells["B3"].Value = sd; // Start Date
            ws.Cells["B4"].Value = ed; // End Date
            foreach (Reports rep in reps)
            {
                if (rep.StartDate == sd && rep.EndDate == ed)
                    ws.Cells["B5"].Value = rep.AlertsInTimePeriod;
            }
            //ws.Cells["B5"].Value = reps.Find(x => x.StartDate.Contains(sd) && x.EndDate.Contains(ed)).AlertsInTimePeriod; // Alerts Tripped

            List<Alerts> alrts = DatabaseHandler.exportAlerts(customerID, accountID);

            int startRow = 8;
            DateTime startDate = DateTime.ParseExact(sd, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(ed, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            foreach (Alerts alrt in alrts)
            {
                DateTime alrtDate = DateTime.ParseExact(alrt.TransDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                if (startDate <= alrtDate && alrtDate <= endDate)
                {
                    ws.Cells["A" + startRow.ToString()].Value = alrt.TransDate; // Date
                    ws.Cells["B" + startRow.ToString()].Value = alrt.Reason; // Reason for Alert
                    ws.Cells["C" + startRow.ToString()].Value = alrt.TransDesc; // Description
                    ws.Cells["D" + startRow.ToString()].Value = alrt.Location; // Location
                    ws.Cells["E" + startRow.ToString()].Value = Convert.ToDecimal(alrt.TransType + alrt.Amount); // Amount
                    ws.Cells["F" + startRow.ToString()].Value = alrt.Balance; // Balance
                    startRow++;
                }
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            result = pkg.GetAsByteArray();
            string fileName = "export" + sd + "to" + ed + ".xlsx";
            return File(result, "application/vnd.ms-excel", fileName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
