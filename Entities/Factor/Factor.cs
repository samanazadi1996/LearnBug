using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Factor : BaseEntity
    {
        public int userId { get; set; }
        public int contentId { get; set; }
        public DateTime Datetime { get; set; }
        public double Price { get; set; }
        public int Commission { get; set; }
        public virtual Content Content { get; set; }
        public virtual User User { get; set; }

    }
}

