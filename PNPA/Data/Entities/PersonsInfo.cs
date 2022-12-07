using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNPA.Data.Entities
{
    public class PersonsInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        public Guid RankID { get; set; }
        public virtual Rank Rank { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string MiddleName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? Unit { get; set; } = default!;

        public string? OfficeAddress { get; set; } = default!;

        public string? Designation { get; set; } = default!;

        public string? ViberNumber { get; set; }

        public string EmailAddress { get; set; } = default!;

        public string? FacebookNameLink { get; set; } = default!;

        public string? MobileNum { get; set; } = default!;

        public string Gender { get; set; } = default!;

        public DateTime BirthDate { get; set; }

        public Int32 BadgeNumber { get; set; }
    }
}
