using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppAgenceVoyage.Models
{
    public class Utilisateur
    {
        [Key]
        public int IdUtilisateur {  get; set; }
        
        [Display(Name ="Nom"),Required(ErrorMessage ="*"),MaxLength(80)]
        public string NomUtilisateur { get; set; }

        [Display(Name = "Prenom"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string PreomUtilisateur { get; set; }

        [DataType(DataType.EmailAddress )]
        [Display(Name = "Email"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string EmailUtilisateur { get; set; }

        [RegularExpression(@"^(77|78|70|75|76)[0-9]{7}$", ErrorMessage = "Le numéro de téléphone doit commencer par 77, 78, 70, 75 ou 76 et être suivi de 7 chiffres.")]
        [Display(Name = "Telephone"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string TelephoneUtilisateur { get; set; }

    }
}