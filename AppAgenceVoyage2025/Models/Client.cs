using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppAgenceVoyage.Models
{
    public class Client:Utilisateur
    {
        [RegularExpression(@"^(1|2) \d{4} \d{5}$", ErrorMessage = "Le CNI doit commencer par 1 ou 2, suivi d'un espace, de 4 chiffres, un autre espace, et de 5 chiffres.")]

        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIClient { get; set; }
    }
}