using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.ViewModel.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool IsPersistent { get; set; } = false;

        public string ReturnUrl { get; set; }
    }
}
