using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    [Table("Factor")]

    public class Factor : BaseEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Factor>
        {
            public Configuration()
            {

                HasRequired(current => current.Content).
                    WithMany(current => current.Factors).
                    HasForeignKey(current => current.contentId)
                    .WillCascadeOnDelete(false);

                HasRequired(current => current.User)
                    .WithMany(current => current.Factors)
                    .HasForeignKey(current => current.userId)
                    .WillCascadeOnDelete(false);

            }
        }
        #endregion /Configuration

        #region Constractor
        public Factor()
        {

        }
        #endregion

        public DateTime Datetime { get; set; }
        public double Price { get; set; }
        public int Commission { get; set; }

        #region Content
        public virtual Content Content { get; set; }
        public int contentId { get; set; }
        #endregion

        #region User
        public virtual User User { get; set; }
        public int userId { get; set; }
        #endregion

    }
}

