using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Message : BaseEntity
    {
        public int FromuserId { get; set; }
        public int TouserId { get; set; }
        public string Text { get; set; }
        public DateTime Datetime { get; set; }
        public Nullable<long> Status { get; set; }

        public virtual User User { get; set; }
        public virtual User User1 { get; set; }

    }
}

