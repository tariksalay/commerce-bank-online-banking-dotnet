using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace asp_core_mvc.Models
{
    public static class DatabaseHandler
    {
        private static string connectionString;

        public static void SetupConnection(IConfiguration config)
        {
            connectionString = config.GetConnectionString("BankDatabase");
            if (String.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Configure ConnectionStrings:BankDatabase before starting the app.");
        }

        private static MySqlConnection CreateConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static List<Alerts> getAlerts(Int32 customerID, Int32 accountID)
        {
            List<Alerts> alerts = new List<Alerts>();

            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAlerts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    cmd.Parameters.AddWithValue("@accountID", accountID);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if(!Convert.ToBoolean(rdr["Removed"]))
                            {
                                Alerts alert = new Alerts();
                                alert.AlertID = Convert.ToInt32(rdr["ID"]);
                                alert.TransId = Convert.ToInt32(rdr["TransactionID"]);
                                alert.TransDate = rdr["Date"].ToString();
                                alert.TransDesc = rdr["Description"].ToString();
                                if (rdr["Type"].ToString() == "CR") // Deposit
                                alert.TransType = "";
                                else if (rdr["Type"].ToString() == "DR") // Withdrawal
                                alert.TransType = "-";
                                alert.Location = rdr["Location"].ToString();
                                alert.Amount = Convert.ToDouble(rdr["Amount"]);
                                alert.Balance = Convert.ToDouble(rdr["Balance"]);
                                alert.Reason = rdr["Reason"].ToString();
                                alerts.Add(alert);
                            }
                        }
                    }
                }
            }

            return alerts;
        }

        public static List<Alerts> exportAlerts(Int32 customerID, Int32 accountID)
        {
            List<Alerts> alerts = new List<Alerts>();

            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAlerts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    cmd.Parameters.AddWithValue("@accountID", accountID);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Alerts alert = new Alerts();
                            alert.AlertID = Convert.ToInt32(rdr["ID"]);
                            alert.TransId = Convert.ToInt32(rdr["TransactionID"]);
                            alert.TransDate = Convert.ToDateTime(rdr["Date"]).ToString("MM/dd/yyyy");
                            alert.TransDesc = rdr["Description"].ToString();
                            if (rdr["Type"].ToString() == "CR") // Deposit
                            alert.TransType = "";
                            else if (rdr["Type"].ToString() == "DR") // Withdrawal
                            alert.TransType = "-";
                            alert.Location = rdr["Location"].ToString();
                            alert.Amount = Convert.ToDouble(rdr["Amount"]);
                            alert.Balance = Convert.ToDouble(rdr["Balance"]);
                            alert.Reason = rdr["Reason"].ToString();
                            alert.Removed = Convert.ToBoolean(rdr["Removed"]);
                            alerts.Add(alert);
                        }
                    }
                }
            }

            return alerts;
        }

        public static List<Transactions> getTransactions(Int32 accountID)
        {
            List<Transactions> transactions = new List<Transactions>();

            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("getTransactions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accountID", accountID);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Transactions transaction = new Transactions();
                            transaction.TransId = Convert.ToInt32(rdr["ID"]);
                            transaction.TransDate = Convert.ToDateTime(rdr["Date"]).ToString("MM/dd/yyyy");
                            transaction.TransDesc = rdr["Description"].ToString();
                            if (rdr["Type"].ToString() == "CR") // Deposit
                            transaction.TransType = "";
                            else if (rdr["Type"].ToString() == "DR") // Withdrawal
                            transaction.TransType = "-";
                            transaction.Location = rdr["Location"].ToString();
                            transaction.Amount = Convert.ToDouble(rdr["Amount"]);
                            transaction.Balance = Convert.ToDouble(rdr["Balance"]);
                            transactions.Add(transaction);
                        }
                    }
                }
            }

            return transactions;
        }

        public static List<Reports> getReports(Int32 customerID, Int32 accountID)
        {
            List<Reports> reports = new List<Reports>();

            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("getReports", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    cmd.Parameters.AddWithValue("@accountID", accountID);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if(Convert.ToInt32(rdr["OutState"]) > 0)
                            reports.Add(new Reports("Out of State", Convert.ToInt32(rdr["OutState"])));
                            if (Convert.ToInt32(rdr["Range"]) > 0)
                            reports.Add(new Reports("Flagged date range", Convert.ToInt32(rdr["Range"])));
                            if (Convert.ToInt32(rdr["Category"]) > 0)
                            reports.Add(new Reports("Flagged catagory", Convert.ToInt32(rdr["Category"])));
                            if (Convert.ToInt32(rdr["ExcessTran"]) > 0)
                            reports.Add(new Reports("Excessive Transaction", Convert.ToInt32(rdr["ExcessTran"])));
                            if (Convert.ToInt32(rdr["ExcessDepo"]) > 0)
                            reports.Add(new Reports("Excessive Deposit", Convert.ToInt32(rdr["ExcessDepo"])));
                            if (Convert.ToInt32(rdr["ExcessWith"]) > 0)
                            reports.Add(new Reports("Excessive Withdraw", Convert.ToInt32(rdr["ExcessWith"])));
                            if (Convert.ToInt32(rdr["HighBal"]) > 0)
                            reports.Add(new Reports("High Balance", Convert.ToInt32(rdr["HighBal"])));
                            if (Convert.ToInt32(rdr["LowBal"]) > 0)
                            reports.Add(new Reports("Low Balance", Convert.ToInt32(rdr["LowBal"])));
                        }
                    }
                }
            }

            return reports;
        }

        public static List<Reports> getPrevReports(Int32 customerID, Int32 accountID)
        {
            List<Reports> prevReports = new List<Reports>();

            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("getPrevReports", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    cmd.Parameters.AddWithValue("@accountID", accountID);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Reports prevReport = new Reports();
                            prevReport.StartDate = Convert.ToDateTime(rdr["Start"]).ToString("MM/dd/yyyy");
                            prevReport.EndDate = Convert.ToDateTime(rdr["End"]).ToString("MM/dd/yyyy");
                            prevReport.AlertsInTimePeriod = Convert.ToInt32(rdr["AlertsInTimePeriod"]);
                            prevReports.Add(prevReport);
                        }
                    }
                }
            }

            return prevReports;
        }

        public static Rules getRules(Int32 customerID, Int32 accountID)
        {
            Rules rules = new Rules();
            Boolean foundStart = false, foundEnd = false;
            rules.accountID = accountID;

            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("getRules", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    cmd.Parameters.AddWithValue("@accountID", accountID);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            rules.OutStateTrans = Convert.ToBoolean(rdr["OutStateChk"]);
                            rules.rangeTrans = Convert.ToBoolean(rdr["RangeChk"]);

                            if(rdr["Start"] != DBNull.Value)
                            {
                                rules.startTrans = Convert.ToDateTime(rdr["Start"]);
                                foundStart = true;
                            }
                            if(rdr["End"] != DBNull.Value)
                            {
                                rules.endTrans = Convert.ToDateTime(rdr["End"]);
                                foundEnd = true;
                            }

                            rules.catTrans = Convert.ToBoolean(rdr["CatChk"]);
                            rules.catTxt = rdr["Category"].ToString();
                            rules.greatTrans = Convert.ToBoolean(rdr["GTranChk"]);
                            rules.greatTransAmt = Convert.ToInt32(rdr["GTranAmt"]);
                            rules.greatDepo = Convert.ToBoolean(rdr["GDepoChk"]);
                            rules.greatDepoAmt = Convert.ToDouble(rdr["GDepoAmt"]);
                            rules.greatWithdraw = Convert.ToBoolean(rdr["GWithChk"]);
                            rules.greatWithdrawAmt = Convert.ToDouble(rdr["GWithAmt"]);
                            rules.greatBal = Convert.ToBoolean(rdr["GBalChk"]);
                            rules.greatBalAmt = Convert.ToDouble(rdr["GBalAmt"]);
                            rules.lessBal = Convert.ToBoolean(rdr["LBalChk"]);
                            rules.lessBalAmt = Convert.ToDouble(rdr["LBalAmt"]);
                        }

                        if(!foundStart)
                        rules.startTrans = DateTime.Today;
                        if (!foundEnd)
                        rules.endTrans = DateTime.Today;
                    }
                }
            }

            return rules;
        }

        public static void setRules(Int32 customerID, Rules rules)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("setRules", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OutStateChk", rules.OutStateTrans);
                    cmd.Parameters.AddWithValue("@RangeChk", rules.rangeTrans);
                    cmd.Parameters.AddWithValue("@Start", rules.startTrans);
                    cmd.Parameters.AddWithValue("@End", rules.endTrans);
                    cmd.Parameters.AddWithValue("@CatChk", rules.catTrans);
                    cmd.Parameters.AddWithValue("@Category", rules.catTxt);
                    cmd.Parameters.AddWithValue("@GTranChk", rules.greatTrans);
                    cmd.Parameters.AddWithValue("@GTranAmt", rules.greatTransAmt);
                    cmd.Parameters.AddWithValue("@GDepoChk", rules.greatDepo);
                    cmd.Parameters.AddWithValue("@GDepoAmt", rules.greatDepoAmt);
                    cmd.Parameters.AddWithValue("@GWithChk", rules.greatWithdraw);
                    cmd.Parameters.AddWithValue("@GWithAmt", rules.greatWithdrawAmt);
                    cmd.Parameters.AddWithValue("@GBalChk", rules.greatBal);
                    cmd.Parameters.AddWithValue("@GBalAmt", rules.greatBalAmt);
                    cmd.Parameters.AddWithValue("@LBalChk", rules.lessBal);
                    cmd.Parameters.AddWithValue("@LBalAmt", rules.lessBalAmt);
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    cmd.Parameters.AddWithValue("@accountID", rules.accountID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void deleteRules(Int32 customerID, Int32 accountID)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("deleteRules", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    cmd.Parameters.AddWithValue("@accountID", accountID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void removeAlert(int AlertID)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("removedAlert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AlertID", AlertID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Boolean validateUser(LoginModel login)
        {
            Boolean validUser = false;

            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("validateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", login.UserName);
                    cmd.Parameters.AddWithValue("@password", login.Password);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            validUser = true;
                            login.CustomerID = Convert.ToInt32(rdr["ID"]);
                            login.FullName = rdr["FirstName"].ToString() + " " + rdr["LastName"].ToString();
                        }
                    }
                }
            }

            return validUser;
        }

        public static List<Int32> getAccounts(Int32 customerID)
        {
            List<Int32> accountID = new List<Int32>();

            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAccounts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            accountID.Add(Convert.ToInt32(rdr["AccountID"]));
                        }
                    }
                }
            }

            return accountID;
        }

        public static Account getAccount(Int32 accountID)
        {
            Account account = new Account();

            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAccount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accountID", accountID);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            account.AccountType = rdr["Type"].ToString();
                            account.Balance = Convert.ToDouble(rdr["Balance"]);
                        }
                    }
                }
            }

            return account;
        }
    }
}
