using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="نام و نام خانوادگی را وارد کنید")]
        [Display(Name ="نام و نام خانوادگی")]
        [MaxLength(100,ErrorMessage ="نام و نام خانوادگی نباید بیش از 100 کاراکتر باشد")]

        public string FullName { get; set; }

        [Required(ErrorMessage ="ایمیل را وارد کنید")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="ایمیل")]
        public string Email { get; set; }


        [Required(ErrorMessage = "پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد")]
        public string Password { get; set; }

 
        [Required(ErrorMessage = "تکرار پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="پسورد و تکرار آن باید برابر باشد")]
        [Display(Name = "تکرار پسورد")]
        public string RePassword { get; set; }

        [Display(Name = "تلفن")]

        [DataType(DataType.PhoneNumber)]
        public string  PhoneNumber { get; set; }
    }
}
