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
    [Table("Comment")]
    public class Comment : BaseEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Comment>
        {
            public Configuration()
            {
                HasRequired(current => current.User)
                    .WithMany(current => current.Comments)
                    .HasForeignKey(current => current.userId)
                    .WillCascadeOnDelete(false);

                HasRequired(current => current.Content)
                    .WithMany(current => current.Comments)
                    .HasForeignKey(current => current.contentId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region Constractor
        public Comment()
        {

        }
        #endregion

        public string Text { get; set; }
        public DateTime Datetime { get; set; }

        #region User
        public virtual User User { get; set; }
        public int userId { get; set; }
        #endregion   

        #region Content
        public virtual Content Content { get; set; }
        public int contentId { get; set; }
        #endregion

    }
}

