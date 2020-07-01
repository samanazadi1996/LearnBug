using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    [Table("Setting")]

    public class Setting : IEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Setting>
        {
            public Configuration()
            {
            }
        }
        #endregion /Configuration

        #region Constractor
        public Setting()
        {

        }
        #endregion

        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}

