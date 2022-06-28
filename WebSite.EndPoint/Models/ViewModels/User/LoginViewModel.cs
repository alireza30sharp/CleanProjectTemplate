using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        public bool RememberMe { get; set; }
        [Required(ErrorMessage = "پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
