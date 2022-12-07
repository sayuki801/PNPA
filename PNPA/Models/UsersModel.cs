using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PNPA.Models
{
    [DataContract]
    public class UsersModel
    {

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        public string Username { get; set; } = default!;

        [DataMember]
        [Required]
        public string Password { get; set; } = default!;

        [DataMember]
        public string ConfirmPassword { get; set; } = default!;

        [DataMember]
        public RoleModel Role { get; set; } = default!;

        public UsersModel()
        {
            Role = new RoleModel();
        }
    }
}
