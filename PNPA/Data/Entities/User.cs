using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNPA.Data.Entities
{
    public class User
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Guid RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual Role Roles { get; set; } = default!;
    }
}
