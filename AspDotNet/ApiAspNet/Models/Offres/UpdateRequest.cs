using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Offres
{
    public class UpdateRequestO
    {
        [MaxLength(2000)]
        public string DescriptionOffre { get; set; }

        public float PrixJourOffre { get; set; }

        [MaxLength(20)]
        public string Disponibilite { get; set; }

        public int IdAgence { get; set; }
    }
}
