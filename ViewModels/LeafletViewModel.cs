using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LeafletViewModel
    {
        [Display(Name = "عرض جغرافیایی")]
        [DisplayName("عرض جغرافیایی")]
        public double Lat { get; set; }
        [Display(Name = "طول جغرافیایی")]
        [DisplayName("طول جغرافیایی")]
        public double Lng { get; set; }
    }
}
