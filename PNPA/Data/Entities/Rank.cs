using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNPA.Data.Entities
{
    public class Rank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        public string RankABBR { get; set; } = default!;

        public string Description { get; set; } = default!;

        public virtual List<PersonsInfo> PersonsInfo { get; set; } = default!;
    }
}
