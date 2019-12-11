using System;

namespace asp_core_mvc.Models
{
    public class Reports
    {
        public Reports() { }
        public Reports(string rule, int occurances) {
            RuleReport = rule;
            TimesRecently = occurances;
        }

        public string RuleReport { get; set; }
        public int TimesRecently { get; set; }

        // previous reports
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int AlertsInTimePeriod { get; set; }
    }
}