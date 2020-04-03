using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Content : BaseEntity
    {


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
        [DisplayName("گروه مطلب")]
        [Display(Name = "گروه مطلب")]
        [Required(ErrorMessage = "گروه مطلب را مشخص کنید")]
        public int groupId { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("درج شده توسط")]
        [Display(Name = "درج شده توسط")]
        public int userId { get; set; }

        public Nullable<double> Price { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Factor> Factors { get; set; }

    }
}

