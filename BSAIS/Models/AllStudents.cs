using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSAIS.Models
{
    [Keyless]
    public class AllStudents
    {
        [Column("CourseId")]
        public string? CourseId { get; set; }

        [Column("CourseName")]
        public string? CourseName { get; set; }

        [Column("StudentId")]
        public string? StudentId { get; set; }

        [Column("Firstname")]
        public string? Firstname { get; set; }

        [Column("Midname")]
        public string? Midname { get; set; }

        [Column("Lastname")]
        public string? Lastname { get; set; }

        [Column("DateEnrolled")]
        public DateTime? DateEnrolled { get; set; }

        [Column("SchoolYear")]
        public int? SchoolYear { get; set; }

        [Column("SubjectId")]
        public string? SubjectId { get; set; }

        [Column("Description")]
        public string? Description { get; set; }

        [Column("Prelim")]
        public decimal Prelim { get; set; }

        [Column("Midterm")]
        public decimal Midterm { get; set; }

        [Column("Semi")]
        public decimal Semi { get; set; }

        [Column("Final")]
        public decimal Final { get; set; }

        [Column("Credits")]
        public decimal? Credits { get; set; }

        [Column("StudentAddress")]
        public string? StudentAddress { get; set; }

        public int Rating
        {
            get {
                if (Prelim > 0 && Midterm > 0 && Semi > 0 && Final > 0)
                    return (int)Math.Floor((Prelim + Midterm + Semi + Final) / 4M);
                else
                    return 0;
            }
        }
    }
}
