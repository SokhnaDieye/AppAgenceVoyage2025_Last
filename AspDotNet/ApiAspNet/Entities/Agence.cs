﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAspNet.Entities
{
    public class Agence
    {
        [Key]
        public int IdAgence { get; set; }
        [Display(Name = "Adresse"), Required(ErrorMessage = "*"), MaxLength(150)]
        public string AdresseAgence { get; set; }
        [Display(Name = "Longitude")]
        public float? Longitude { get; set; }
        [Display(Name = "Latitude")]
        public float? Latitude { get; set; }
        [Display(Name = "Ninea"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string NineaGestionnaire { get; set; }
        [Display(Name ="RCCM"),Required(ErrorMessage ="*"),MaxLength(20)]
        public string RccmGestionnaire { get; set; }

        
        public int IdGestionnaire { get; set; }

        [ForeignKey("IdGestionnaire")]
        public virtual Gestionnaire Gestionnaire { get; set; }


        public virtual ICollection<Offre> Offres { get; set; }

    }
}