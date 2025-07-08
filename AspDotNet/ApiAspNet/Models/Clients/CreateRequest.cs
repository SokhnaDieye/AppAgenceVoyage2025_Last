using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Clients
{
    public class CreateRequestCl : Users.CreateRequests
    {
        [Required]
        [MaxLength(20)]
        public string CNIClient { get; set; }
    }
}
