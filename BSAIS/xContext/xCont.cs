
using BSAIS.Models;
using Microsoft.EntityFrameworkCore;

public class xCont : DbContext
{
    public xCont(DbContextOptions<xCont> xContOptions) : base(xContOptions) { }
    public DbSet<Logins> logs { get; set; }
    public DbSet<AllStudents> studentx { get; set; }
    public DbSet<Courses> coursesx { get; set; }
    public DbSet<StudentDetails> studentDetails { get; set; }
    public DbSet<GetStudents> getStudents { get; set; }
    public DbSet<StairwayToCourses> Stairways { get; set; }
    public DbSet<Subjects> Subs { get; set; }
}
