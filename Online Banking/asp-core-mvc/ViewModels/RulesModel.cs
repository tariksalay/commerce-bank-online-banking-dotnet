using System;
using System.Collections.Generic;
using asp_core_mvc.Models;

namespace asp_core_mvc.ViewModels
{
    public class RulesModel
    {
        public Int32 curAccount { get; set; }
        public IEnumerable<Int32> accounts { get; set; }
        public Rules rules { get; set; }
    }
}
