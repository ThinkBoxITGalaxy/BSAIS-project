
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[Keyless]
public class Courses
{
    [Column("CourseId")]
    public string id { get; set; }

    [Column("CourseName")]
    public string coursename { get; set; }
}
