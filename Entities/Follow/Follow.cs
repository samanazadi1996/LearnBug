using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Follow : BaseEntity
    {
        public int MyUserId { get; set; }
        public int followUserId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual User User { get; set; }
        public virtual User User1 { get; set; }

    }
}

