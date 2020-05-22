using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    [Table("Follow")]
    public class Follow : BaseEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Follow>
        {
            public Configuration()
            {
                HasRequired(current => current.Follower)
                    .WithMany(current => current.Follower)
                    .HasForeignKey(current => current.followerId)
                    .WillCascadeOnDelete(false);

                HasRequired(current => current.Following)
                    .WithMany(current => current.Following)
                    .HasForeignKey(current => current.followingId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region Constractor
        public Follow()
        {

        }
        #endregion

        public DateTime DateTime { get; set; }

        #region User
        public virtual User Follower { get; set; }
        public int followerId { get; set; }

        public virtual User Following { get; set; }
        public int followingId { get; set; }

        #endregion


    }
}

