using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Comment : BaseEntity
    {
        public int userId { get; set; }
        public int contentId { get; set; }
        public string Text { get; set; }
        public DateTime Datetime { get; set; }

        public virtual Content Content { get; set; }
        public virtual User User { get; set; }

    }
}

