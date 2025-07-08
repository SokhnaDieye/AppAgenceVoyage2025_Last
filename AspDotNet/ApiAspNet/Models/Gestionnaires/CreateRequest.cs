using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Gestionnaires
{
    public class CreateRequestG : Users.CreateRequests
    {
        [Required]
        [MaxLength(20)]
        public string CNIGestionnaire { get; set; }
    }
}
