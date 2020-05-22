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
    [Table("Bookmark")]
    public class Bookmark : BaseEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Bookmark>
        {
            public Configuration()
            {
                HasRequired(current => current.User)
                    .WithMany(current => current.Bookmarks)
                    .HasForeignKey(current => current.userId)
                    .WillCascadeOnDelete(false);

                HasRequired(current => current.Content)
                    .WithMany(current => current.Bookmarks)
                    .HasForeignKey(current => current.contentId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region Constractor
        public Bookmark()
        {

        }
        #endregion

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

