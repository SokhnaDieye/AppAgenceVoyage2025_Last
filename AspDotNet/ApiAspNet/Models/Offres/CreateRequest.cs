using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Offres
{
    public class CreateRequestO
    {
        [Required]
        [MaxLength(2000)]
        public string DescriptionOffre { get; set; }

        [Required]
        public float PrixJourOffre { get; set; }

        [MaxLength(20)]
        public string Disponibilite { get; set; }

        [Required]
        public int IdAgence { get; set; }
    }
}
