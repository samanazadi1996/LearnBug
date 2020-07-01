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
    [Table("Group")]
    public class Group : IEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Group>
        {
            public Configuration()
            {
            }
        }
        #endregion /Configuration

        #region Constractor
        public Group()
        {
            this.Posts = new HashSet<Post>();

        }
        #endregion

        public string Name { get; set; }
        public Nullable<int> targetId { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}

