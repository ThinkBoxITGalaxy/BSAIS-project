
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[Keyless]
public class StairwayToCourses
{
    [Column("CourseId")]
    public string? CourseId { get; set; }

    [Column("CourseName")]
    public string? CourseName { get; set; }

    [Column("SubjectId")]
    public string? SubjectId { get; set; }

    [Column("Description")]
    public string? Description { get; set; }
    [Column("Credits")]
    public decimal? Credits { get; set; }
}

