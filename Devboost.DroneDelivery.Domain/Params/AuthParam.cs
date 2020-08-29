using System.ComponentModel.DataAnnotations;

namespace Devboost.DroneDelivery.Domain.Params
{
    public class AuthParam
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }
        
    }
}