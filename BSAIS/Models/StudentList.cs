
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[Keyless]
public class StudentList
{
    [Column("StudentId")]
    public string? StudentId { get; set; }

    [Column("CourseName")]
    public string? CourseName { get; set; }
    [Column("CourseId")]
    public string? CourseId { get; set; }

    [Column("Firstname")]
    public string? Firstname { get; set; }

    [Column("Midname")]
    public string? Midname { get; set; }

    [Column("Lastname")]
    public string? Lastname { get; set; }
    [Column("Sex")]
    public string? Sex { get; set; }
    [Column("StudentAddress")]
    public string? StudentAddress { get; set; }
    [Column("pword")]
    public string? pword { get; set; }
}
