using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppAgenceVoyage2025.Models
{
 
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nom"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string Nom { get; set; }

        [Display(Name = "Type"), Required(ErrorMessage = "*"), MaxLength(10)]
        public string Type { get; set; }

        [Display(Name = "Sexe"), Required(ErrorMessage = "*")]
        public string Sexe { get; set; }

        [Display(Name = "Poids (kg)"), Required(ErrorMessage = "*")]
        [Range(0.1, 1000, ErrorMessage = "Le poids doit être compris entre 0.1 et 1000 kg")]
        public double Poids { get; set; }

        [Display(Name = "Taille (cm)"), Required(ErrorMessage = "*")]
        [Range(1, 300, ErrorMessage = "La taille doit être comprise entre 1 et 300 cm")]
        public int Taille { get; set; }

        [Display(Name = "Date de Naissance"), Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }
    }


}
