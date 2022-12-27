using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PNPA.Models
{
    [DataContract]
    public class RankModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string RankABBR { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;
    }
}
