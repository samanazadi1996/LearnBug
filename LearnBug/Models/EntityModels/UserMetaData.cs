using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Input;

namespace LearnBug.Models.EntityModels
{
    internal class UserMetaData
    {

        [ScaffoldColumn(false)]
        [Required]
        [Key]
        public int Id { get; set; }

        [Display(Name = " : نام کاربری")]
        [DisplayName(" : نام کاربری")]
        [Required(ErrorMessage = "نام کاربری خود را وارد کنید")]
        public string Username { get; set; }

        [Display(Name = " : نام")]
        [DisplayName(": نام")]
        [Required(ErrorMessage = "نام خود را وارد کنید")]
        public string name { get; set; }

        [Display(Name = "ایمیل")]
        [DisplayName("ایمیل")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [DisplayName("رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "تاریخ تولد")]
        [DisplayName("تاریخ تولد")]
        public string Dateofbirth { get; set; }

        [Display(Name = "جنسیت")]
        [DisplayName("جنسیت")]
        public bool Gender { get; set; }

        [Display(Name = "بیوگرافی")]
        [DisplayName("بیوگرافی")]
        public string Biography { get; set; }

        [Display(Name = "شماره تلفن")]
        [DisplayName("شماره تلفن")]
        public string Phone { get; set; }

        [Display(Name = "عکس پروفایل")]
        [DisplayName("عکس پروفایل")]
        public string Image { get; set; }

        [Display(Name = "محلس سکونت")]
        [DisplayName("محل سکونت")]
        public string Location { get; set; }

        [Display(Name = "نوع دستریس")]
        [DisplayName("نوع دسترسی")]
        [ScaffoldColumn(false)]
        public string Roles { get; set; }
        [Display(Name = "وضعیت")]
        [DisplayName("وضعیت")]
        [ScaffoldColumn(false)]
        public Nullable<byte> Status { get; set; }

        [Display(Name = "کیف پول")]
        [DisplayName("کیف پول")]
        public double Wallet { get; set; }

        [Display(Name = "کمیسیون")]
        [DisplayName("کمیسیون")]
        [Range(minimum: 0, maximum: 100)]
        public int Commission { get; set; }

    }

}

namespace LearnBug.Models.DomainModels
{
    [MetadataType(typeof(LearnBug.Models.EntityModels.UserMetaData))]
    public partial class User
    {
        //[Display(Name = "تکرار رمز عبور")]
        //[DisplayName(" ت کراررمز عبور")]
        //[Compare("Password")]

        //public string ConfirmPassword { get; set; }

    }
}


