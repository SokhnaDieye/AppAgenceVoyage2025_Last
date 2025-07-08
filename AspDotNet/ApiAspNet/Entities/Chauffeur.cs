using System.ComponentModel.DataAnnotations;



namespace ApiAspNet.Entities
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