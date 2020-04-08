using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Setting : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}

