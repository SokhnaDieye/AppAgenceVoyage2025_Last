using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetierAgenceVoyage.Models
{
    public class Offre
    {
        [Key]
        public int IdOffre { get; set; }

        [Display(Name = "Description"), Required(ErrorMessage = "*"), MaxLength(2000)]
        public string DescriptionOffre { get; set; }

        [Display(Name = "Prix journalier"), Required(ErrorMessage = "*")]
        public float PrixJourOffre { get; set; }

        [Display(Name = "Disponibilité"), MaxLength(20)]
        public string Disponibilite { get; set; }

        [Required] 
        public int IdAgence { get; set; }

        [ForeignKey("IdAgence")]
        public virtual Agence Agence { get; set; }

    }
}