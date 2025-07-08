using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Chauffeurs
{
    public class UpdateRequestC
    {
        [Required, MaxLength(80)]
        public string Nom { get; set; }

        [Required, MaxLength(80)]
        public string Prenom { get; set; }
    }
}
