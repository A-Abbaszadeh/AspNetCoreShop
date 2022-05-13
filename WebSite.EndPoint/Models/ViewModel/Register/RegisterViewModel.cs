using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.ViewModel.Register
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام و نام خانوادگی را وارد کنید")]
        [MaxLength(100, ErrorMessage = "حداکثر طول نام و نام خانوادگی 100 کاراکتر است")]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "پسورد و تکرار آن باید برابر باشند")]
        [Display(Name = "تکرار پسورد")]
        public string Repassword { get; set; }

        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }
    }
}
