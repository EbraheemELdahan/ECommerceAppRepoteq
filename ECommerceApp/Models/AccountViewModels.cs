﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ECommerceApp.Models
{
    public class LoginRegister
    {
        public LoginViewModel Login { get; set; }
        public RegisterViewModel Register { get; set; }
    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel

    {
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        [Required(ErrorMessage = "FirstName is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "FirstName must be 3 to 50 chars")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is Required")]

        [StringLength(50, MinimumLength = 3, ErrorMessage = "FirstName must be 3 to 50 chars")]
        public string LastName { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "FirstName must be 3 to 50 chars")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "this field is required")]
        [DataType(DataType.Date)]
        public System.DateTime  DaetOfBirth { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required (ErrorMessage ="this field is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "this field is required")]
        [Display(Name ="User Profile")]
        public string UserImg { get; set; }
    }
    

    public class ResetPasswordViewModel
    {
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
