using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Gestionnaires
{
    public class UpdateRequestG : Users.UpdateRequests
    {
        [MaxLength(20)]
        public string CNIGestionnaire { get; set; }
    }
}
