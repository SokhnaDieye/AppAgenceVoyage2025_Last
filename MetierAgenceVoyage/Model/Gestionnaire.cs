using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MetierAgenceVoyage.Models
{
    public class Gestionnaire : Utilisateur
    {
        
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIGestionnaire { get; set; }

        public int IdAgence { get; set; }

        [ForeignKey("IdAgence")]
        public virtual Agence Agence { get; set; }
        public ICollection<Agence> Agences { get; set; }


    }
}