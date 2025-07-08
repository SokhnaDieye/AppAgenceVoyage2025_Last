using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppAgenceVoyage2025.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
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

        [Display(Name = "Mémoriser ce navigateur ?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser mes informations")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nom")]
        [MaxLength(80)]
        public string NomUtilisateur { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        [MaxLength(80)]
        public string PreomUtilisateur { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        [MaxLength(80)]
        public string EmailUtilisateur { get; set; }

        [Required]
        [RegularExpression(@"^(77|78|70|75|76)[0-9]{7}$", ErrorMessage = "Le numéro de téléphone doit commencer par 77, 78, 70, 75 ou 76 et être suivi de 7 chiffres.")]
        [Display(Name = "Téléphone")]
        [MaxLength(20)]
        public string TelephoneUtilisateur { get; set; }

        [Required]
        [RegularExpression(@"^(1|2) \d{4} \d{5}$", ErrorMessage = "Le CNI doit commencer par 1 ou 2, suivi d'un espace, de 4 chiffres, un autre espace, et de 5 chiffres.")]
        [Display(Name = "CNI")]
        [MaxLength(20)]
        public string CNIClient { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} doit contenir au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
