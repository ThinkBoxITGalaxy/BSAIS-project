
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[Keyless]
public class GetStudents
{
    [Column("CourseName")]
    public string coursename { get; set; }

    [Column("StudentId")]
    public string id { get; set; }

    [Column("Firstname")]
    public string fname { get; set; }

    [Column("Midname")]
    public string midname { get; set; }

    [Column("Lastname")]
    public string lname { get; set; }

    [Column("Sex")]
    public string gender { get; set; }
}
