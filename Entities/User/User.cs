using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : BaseEntity
    {


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
        public DateTime Dateofbirth { get; set; }

        [Display(Name = "جنسیت")]
        [DisplayName("جنسیت")]
        public GenderType Gender { get; set; }

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
        public virtual ICollection<Bookmark> Bookmarks { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Content> Contents { get; set; }

        public virtual ICollection<Factor> Factors { get; set; }

        public virtual ICollection<Follow> Follows { get; set; }

        public virtual ICollection<Follow> Follows1 { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Message> Messages1 { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }

    public enum GenderType
    {
        نامشخص = 0,
        مرد = 1,
        زن = 2
    }
}

