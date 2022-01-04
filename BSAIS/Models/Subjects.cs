using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSAIS.Models
{
    [Keyless]
    public class Subjects
    {
        [Column("SubjectId")]
        public string SubjectId { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Credits")]
        public decimal Credits { get; set; }
    }
}
