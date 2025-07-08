using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Clients
{
    public class UpdateRequestCl : Users.UpdateRequests
    {
        [MaxLength(20)]
        public string CNIClient { get; set; }
    }
}
