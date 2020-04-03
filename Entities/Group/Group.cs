using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public Nullable<int> targetId { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Content> Contents { get; set; }

    }
}

