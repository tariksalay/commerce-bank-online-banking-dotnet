using System;
using System.Collections.Generic;
using asp_core_mvc.Models;

namespace asp_core_mvc.ViewModels
{
    public class HomeModel
    {
        public double Balance { get; set; }
        public Int32 curAccount { get; set; }
        public IEnumerable<Int32> accounts { get; set; }
        public IEnumerable<Alerts> Alerts { get; set; }
        public IEnumerable<Transactions> Transactions { get; set; }
    }
}
