using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models.Entities
{
    [Table("Post")]
    public class Post : BaseEntity
    {
        #region Configuration
        internal class Configuration : EntityTypeConfiguration<Post>
        {
            public Configuration()
            {
                HasRequired(current => current.User)
                    .WithMany(current => current.Posts)
                    .HasForeignKey(current => current.userId)
                    .WillCascadeOnDelete(false);

                HasRequired(current => current.Group)
                    .WithMany(current => current.Posts)
                    .HasForeignKey(current => current.groupId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region Constractor
        public Post()
        {
            this.Bookmarks = new HashSet<Bookmark>();
            this.Comments = new HashSet<Comment>();
            this.Factors = new HashSet<Factor>();

        }
        #endregion

        [DisplayName("موضوع مطلب")]
        [Display(Name = "موضوع مطلب")]
        [Required(ErrorMessage = "موضوع را مشخص کنید")]
        public string Subject { get; set; }
        [DisplayName("کلمات کلیدی")]
        [Display(Name = "کلمات کلیدی")]
        public string KeyWords { get; set; }
        [AllowHtml]
        public string Content { get; set; }
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

