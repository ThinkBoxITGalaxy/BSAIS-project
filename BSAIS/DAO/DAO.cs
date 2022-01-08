
using BSAIS.Models;
using Microsoft.EntityFrameworkCore;

public class DAO
{
    private readonly xCont cont;

    public DAO(xCont con)
    {
        cont = con;
    }

    public string login(Logins xlog)
    {
        string sql = string.Format(@"select * from student_info where StudentId = '" + xlog.Id + "' and pword = '" + xlog.password + "'");
        var count = cont.logs.FromSqlRaw(sql);
        if (count.Count() > 0)
            if (xlog.Id == "Admin")
            {
                return "Admin";
            }
            else
            {
                return "login";
            }
        else
            return "failed";
    }

    public List<AllStudents> GetAllStudentx(string idnum)
    {
        var data = cont.studentx.FromSqlRaw(@"SELECT       
                                     en.CourseId, 
                                     courses.CourseName, 
                                     en.StudentId, 
                                     si.Firstname, 
                                     si.Midname, 
                                     si.Lastname, 
                                     si.DateEnrolled, 
                                     si.StudentAddress,
                                     en.SchoolYear, 
                                     en.SubjectId, 
                                     subs.[Description], 
                                     ISNULL(en.Prelim, 0) as Prelim, 
                                     ISNULL(en.Midterm, 0) as Midterm, 
                                     ISNULL(en.Semi, 0) as Semi, 
                                     ISNULL(en.Final, 0) as Final, 
                                     subs.Credits 
                                     FROM            
                                     courses INNER JOIN
                                     Enrollment_master as en ON courses.CourseId = en.CourseId INNER JOIN
                                     student_info as si ON en.StudentId = si.StudentId INNER JOIN
                                     subjects as subs ON en.SubjectId = subs.SubjectId " + idnum + "").ToList();
        return data;
    }
    public List<StudentList> StudentList(string id) 
    {
        var data = cont.studentLists.FromSqlRaw(@"SELECT
                                              courses.CourseId, 
                                              courses.CourseName, 
                                              student_info.StudentId, 
                                              student_info.Firstname, 
                                              student_info.Midname, 
                                              student_info.Lastname, 
                                              student_info.Sex, 
                                              student_info.pword,
                                              student_info.StudentAddress
                                              FROM            
                                              student_info INNER JOIN
                                              courses ON student_info.course = courses.CourseId where student_info.StudentId = '" + id + "'").ToList();
        return data;
    }
    public List<GetStudents> GetStudents()
    {
        var walkingZombies = cont.getStudents.FromSqlRaw(@"SELECT        
                                                        courses.CourseName, 
                                                        student_info.StudentId, 
                                                        student_info.Firstname, 
                                                        student_info.Midname, 
                                                        student_info.Lastname,
                                                        student_info.Sex
                                                        FROM            
                                                        courses INNER JOIN
                                                        student_info ON courses.CourseId = student_info.course").ToList();
        return walkingZombies;
    }
    public List<GetStudents> SearchThough(GetStudents LookThroughData)
    {
        var PinPointing = cont.getStudents.FromSqlRaw(@"SELECT        
                                                        courses.CourseName, 
                                                        student_info.StudentId, 
                                                        student_info.Firstname, 
                                                        student_info.Midname, 
                                                        student_info.Lastname,
                                                        student_info.Sex
                                                        FROM            
                                                        courses INNER JOIN
                                                        student_info ON courses.CourseId = student_info.course where student_info.Lastname = '" + LookThroughData.lname + "'").ToList();
        return PinPointing;
    }


    public string AddStudent(StudentDetails student)
    {
        string sql = string.Format(@"Insert into student_info(StudentId, Firstname, Midname, Lastname, Birthdate, Sex, StudentAddress, pword, course) 
                                     values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')", student.idnum, student.fname, student.midname,
                                     student.lname, student.bday, student.gender, student.address, student.pw, student.crs);
        int count = cont.Database.ExecuteSqlRaw(sql);
        if (count > 0)
            return "Added";
        else
            return "Failed";
    }
    public string AddCourses(Courses courses)
    {
        string sql = string.Format(@"Insert into courses(CourseId, CourseName) values ('{0}', '{1}')", courses.id, courses.coursename);
        int ToCheck = cont.Database.ExecuteSqlRaw(sql);
        if (ToCheck > 0)
        {
            return "1";
        }
        else
            return "0";
    }
    public void AddSubs(Subjects sub)
    {
        string sql = string.Format(@"Insert into subjects(SubjectId, [Description], Credits) values ('{0}', '{1}', '{2}')", sub.SubjectId, sub.Description, sub.Credits);
        cont.Database.ExecuteSqlRaw(sql);
    }
    public void SubjectToStudents(AllStudents allStudents)
    {
        string sql = string.Format(@"Insert into Enrollment_master(CourseId, StudentId, SubjectId, Prelim, Midterm, Semi, Final) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", allStudents.CourseId, allStudents.StudentId, allStudents.SubjectId, 0, 0, 0, 0);
        cont.Database.ExecuteSqlRaw(sql);
    }
    public void StudUpdate(AllStudents all)
    {
        string sql = $"Update Enrollment_master set Prelim = '{ all.Prelim }', Midterm = '{ all.Midterm }', Semi = '{ all.Semi }', Final = '{ all.Final }' where StudentId = '{ all.StudentId }' and SubjectId = '{ all.SubjectId }'";
        cont.Database.ExecuteSqlRaw(sql);
    }
    public List<Courses> GetCourses()
    {
        var data = cont.coursesx.FromSqlRaw(@"select * from courses").ToList();
        return data;
    }
    public List<Subjects> GetSubjects(string val)
    {
        if (val == null)
        {
            var subs = cont.Subs.FromSqlRaw("Select * from subjects").ToList();
            return subs;
        }
        else
        {
            var xData = cont.Subs.FromSqlRaw(@"SELECT     
                                                courses.CourseId, 
                                                courses.CourseName, 
                                                subjects.SubjectId, 
                                                subjects.[Description],
                                                subjects.Credits
                                                FROM            
                                                courses 
                                                INNER JOIN
                                                StairwayToCourses ON courses.CourseId = StairwayToCourses.CourseId 
                                                INNER JOIN
                                                subjects ON StairwayToCourses.SubjectId = subjects.SubjectId where CourseName = '" + val + "'").ToList();
            return xData;
        }
    }
    public List<Subjects> StairwayToCoursesx(string values)
    {
        var xData = cont.Subs.FromSqlRaw(@"SELECT
                                                courses.CourseId, 
                                                courses.CourseName, 
                                                subjects.SubjectId, 
                                                subjects.[Description],
                                                subjects.Credits
                                                FROM            
                                                courses 
                                                INNER JOIN
                                                StairwayToCourses ON courses.CourseId = StairwayToCourses.CourseId 
                                                INNER JOIN
                                                subjects ON StairwayToCourses.SubjectId = subjects.SubjectId " + values + "").ToList();
        return xData;
    }
    public List<Subjects> GetSubs()
    {
        var subs = cont.Subs.FromSqlRaw("Select * from subjects").ToList();
        return subs;
    }
    public void DeleteSubs(AllStudents allStudents)
    {
        string sql = $"Delete From Enrollment_master where StudentId = '{allStudents.StudentId}' and SubjectId = '{allStudents.SubjectId}'";
        cont.Database.ExecuteSqlRaw(sql);
    }
}
