using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class RegisterVeiwModel
    {
        [Display(Name = " : نام کاربری")]
        [DisplayName(" : نام کاربری")]
        [Required(ErrorMessage = "نام کاربری خود را وارد کنید")]
        public string Username { get; set; }

        [Display(Name = " : نام")]
        [DisplayName(": نام")]
        [Required(ErrorMessage = "نام خود را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [DisplayName("ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل اشتباه است")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "رمز عبور اجباری است")]

        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [DisplayName("تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور اشتباه است")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "تاریخ تولد")]
        [DisplayName("تاریخ تولد")]
        [Required(ErrorMessage = "تاریخ تولد اجباری است")]
        public string PersianDateofbirth { get; set; }

        [Display(Name = "جنسیت")]
        [DisplayName("جنسیت")]
        public GenderType Gender { get; set; }

        [Display(Name = "بیوگرافی")]
        [DisplayName("بیوگرافی")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [Display(Name = "شماره تلفن")]
        [DisplayName("شماره تلفن")]
        [Required(ErrorMessage = "شماره تلفن اجباری است")]
        public string Phone { get; set; }

        [Display(Name = "محلس سکونت")]
        [DisplayName("محل سکونت")]
        public string Location { get; set; }

        public LeafletViewModel leafletViewwModel { get; set; }
        
    }
}
