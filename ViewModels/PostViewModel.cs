using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Models.Entities;
namespace ViewModels
{
    public class PostViewModel
    {
        [Display(Name ="شناسه")]
        [DisplayName("شناسه")]
        public int Id { get; set; }
        [Display(Name ="مدرس")]
        [DisplayName("مدرس")]
        public string UserName { get; set; }
        [Display(Name = "دسته بندی")]
        [DisplayName("دسته بندی")]
        public string GroupName { get; set; }
        [Display(Name = "موضوع")]
        [DisplayName("موضوع")]
        public string Subject { get; set; }
        [Display(Name = "تعداد نظرات")]
        [DisplayName("تعداد نظرات")]
        public int CommentsCount { get; set; }
        [Display(Name = "ساعت و تاریخ")]
        [DisplayName("ساعت و تاریخ")]
        public DateTime InsertDateTime { get; set; }
        [Display(Name = "کلمات کلیدی")]
        [DisplayName("کلمات کلیدی")]
        public string KeyWords { get; set; }
        [Display(Name = "قیمت")]
        [DisplayName("قیمت")]
        public Nullable<double> Price { get; set; }
        public bool IsMainPost { get; set; }    
        public bool IBuyedPost { get; set; }
    }
}