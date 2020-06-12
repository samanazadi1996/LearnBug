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
    [Table("User")]
    public class User : BaseEntity
    {

        #region Configuration
        internal class Configuration : EntityTypeConfiguration<User>
        {
            public Configuration()
            {

            }
        }
        #endregion /Configuration

        #region Constractor
        public User()
        {
            this.Bookmarks   = new HashSet<Bookmark>();
            this.Posts = new HashSet<Post>();
            this.Comments = new HashSet<Comment>();
            this.Follower = new HashSet<Follow>();
            this.Following = new HashSet<Follow>();
            this.Factors = new HashSet<Factor>();
            this.Transactions = new HashSet<Transaction>();
            this.Sent = new HashSet<Message>();
            this.Inbox = new HashSet<Message>();
        }
        #endregion

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
        [EmailAddress(ErrorMessage ="ایمیل اشتباه است")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "رمز عبور اجباری است")]

        public string Password { get; set; }
        //[Display(Name = "تکرار رمز عبور")]
        //[DisplayName("تکرار رمز عبور")]
        //[NotMapped]
        //[Compare("Password", ErrorMessage = "تکرار رمز عبور اشتباه است")]
        //public string ConfirmPassword { get; set; }

        [Display(Name = "تاریخ تولد")]
        [DisplayName("تاریخ تولد")]
        [Required(ErrorMessage = "تاریخ تولد اجباری است")]
        public DateTime Dateofbirth { get; set; }

        [Display(Name = "جنسیت")]
        [DisplayName("جنسیت")]
        public GenderType Gender { get; set; }

        [Display(Name = "بیوگرافی")]
        [DisplayName("بیوگرافی")]
        public string Biography { get; set; }

        [Display(Name = "شماره تلفن")]
        [DisplayName("شماره تلفن")]
        [Required(ErrorMessage ="شماره تلفن اجباری است") ]
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

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Factor> Factors { get; set; }

        public virtual ICollection<Follow> Follower { get; set; }

        public virtual ICollection<Follow> Following { get; set; }

        public virtual ICollection<Message> Inbox { get; set; }

        public virtual ICollection<Message> Sent { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }



    }

    public enum GenderType
    {
        [Display(Name ="نامشخص")]
        Unknow = 0,
        [Display(Name = "مرد")]
        Mele = 1,
        [Display(Name = "زن")]
        Femele = 2
    }
}