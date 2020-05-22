using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    [Table("Content")]
    public class Content : BaseEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Content>
        {
            public Configuration()
            {
                HasRequired(current => current.User)
                    .WithMany(current => current.Contents)
                    .HasForeignKey(current => current.userId)
                    .WillCascadeOnDelete(false);

                HasRequired(current => current.Group)
                    .WithMany(current => current.Contents)
                    .HasForeignKey(current => current.groupId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region Constractor
        public Content()
        {

        }
        #endregion

        [DisplayName("موضوع مطلب")]
        [Display(Name = "موضوع مطلب")]
        [Required(ErrorMessage = "موضوع را مشخص کنید")]
        public string Subject { get; set; }
        [ScaffoldColumn(false)]
        public DateTime Datetime { get; set; }
        [DisplayName("توضیحات مطلب")]
        [Display(Name = "توضیحات مطلب")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        //[AllowHtml]
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("وضعیت مطلب")]
        [Display(Name = "وضعیت مطلب")]
        public Nullable<byte> Status { get; set; }
        public Nullable<double> Price { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Factor> Factors { get; set; }

        #region Group
        public virtual Group Group { get; set; }
        public int groupId { get; set; }
        #endregion    

        #region User
        public virtual User User { get; set; }
        public int userId { get; set; }
        #endregion


    }
}

