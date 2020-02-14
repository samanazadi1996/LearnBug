using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Models.EntityModels
{
    internal class ContentMetaData
    {
       [ScaffoldColumn(false)]
       [Required]
        public int Id { get; set; }

        [DisplayName("موضوع مطلب")]
        [Display(Name = "موضوع مطلب")]
        [Required(ErrorMessage ="موضوع را مشخص کنید")]
        public string Subject { get; set; }
        [DisplayName("عکس")]
        [Display(Name = "عکس")]
        public string Image { get; set; }
        [ScaffoldColumn(false)]
        public string Datetime { get; set; }
        [DisplayName("توضیحات مطلب")]
        [Display(Name = "توضیحات مطلب")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        [AllowHtml]
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


    }

}

namespace LearnBug.Models.DomainModels
{
    [MetadataType(typeof(LearnBug.Models.EntityModels.ContentMetaData))]
    public partial class Content
    {
    }
}


