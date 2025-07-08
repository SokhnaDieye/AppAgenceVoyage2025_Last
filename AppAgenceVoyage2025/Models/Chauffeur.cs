using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppAgenceVoyage.Models
{
    public class Chauffeur
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nom"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string Nom { get; set; }
        [Display(Name = "Prenom"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string Prenom { get; set; }
    }
}