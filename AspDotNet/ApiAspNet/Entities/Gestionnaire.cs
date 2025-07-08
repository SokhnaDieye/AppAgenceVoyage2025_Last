using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ApiAspNet.Entities
{
    public class Gestionnaire : User
    {
        
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIGestionnaire { get; set; }

        public virtual ICollection<Agence> Agences { get; set; }


    }
}