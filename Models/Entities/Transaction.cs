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
    [Table("Transaction")]

    public class Transaction : BaseEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Transaction>
        {
            public Configuration()
            {
                HasRequired(current => current.User)
                    .WithMany(current => current.Transactions)
                    .HasForeignKey(current => current.userId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region Constractor
        public Transaction()
        {

        }
        #endregion

        public DateTime Datetime { get; set; }
        public double Price { get; set; }
        public bool Charge { get; set; }

        #region User
        public virtual User User { get; set; }
        public int userId { get; set; }
        #endregion
    }
}

