using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transaction : BaseEntity
    {
        public int userId { get; set; }
        public DateTime Datetime { get; set; }
        public double Price { get; set; }
        public bool Charge { get; set; }

        public virtual User User { get; set; }

    }
}

