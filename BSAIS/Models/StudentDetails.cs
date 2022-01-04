
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[Keyless]
public class StudentDetails
{
    [Column("StudentId")]
    public string? idnum { get; set; }

    [Column("Firstname")]
    public string? fname { get; set; }

    [Column("Midname")]
    public string? midname { get; set; }

    [Column("Lastname")]
    public string? lname { get; set; }

    [Column("Birthdate")]
    public DateTime? bday { get; set; }

    [Column("Sex")]
    public string? gender { get; set; }

    [Column("StudentAddress")]
    public string? address { get; set; }

    [Column("pword")]
    public string? pw { get; set; }

    [Column("course")]
    public string? crs { get; set; }
}
