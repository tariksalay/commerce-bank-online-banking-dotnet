using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(45)]
        [Display(Name = "Username")]
        public String UserName { get; set; }

        [Required]
        [StringLength(45)]
        public String Password { get; set; }

        public String FullName { get; set; }

        public Int32 CustomerID { get; set; }
    }
}
