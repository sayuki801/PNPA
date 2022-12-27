using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PNPA.Models
{
    [DataContract]
    public class RoleModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; } = default!;
    }
}
