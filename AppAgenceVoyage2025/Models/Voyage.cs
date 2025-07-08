using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAgenceVoyage.Models
{
    public class Voyage
    {
        [Key]
        public int IdVoyage { get; set; }

        [Display(Name = "Destination"), Required(ErrorMessage = "*"), MaxLength(50)]
        public string Destination { get; set; }
        [Display(Name = "Date Debut")]
        public DateTime DateDebut { get; set; }
        [Display(Name = "Date Fin")]
        public DateTime DateFin {  get; set; }
        [Display(Name = "Prix")]
        public int Prix {  get; set; }


       
    }
}