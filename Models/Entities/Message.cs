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
    [Table("Message")]

    public class Message : BaseEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Message>
        {
            public Configuration()
            {

                HasRequired(current => current.Reciver)
                    .WithMany(current => current.Inbox)
                    .HasForeignKey(current => current.reciverId)
                    .WillCascadeOnDelete(false);

                HasRequired(current => current.sender)
                    .WithMany(current => current.Sent)
                    .HasForeignKey(current => current.senderId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region Constractor
        public Message()
        {

        }
        #endregion

        public string Text { get; set; }
        public DateTime Datetime { get; set; }
        public Nullable<long> Status { get; set; }

        #region User
        public virtual User sender { get; set; }
        public int senderId { get; set; }
        public virtual User Reciver { get; set; }
        public int reciverId { get; set; }

        #endregion


    }
}

