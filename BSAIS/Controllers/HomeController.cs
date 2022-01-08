using BSAIS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BSAIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly DAO dao;
        public HomeController(xCont xc)
        {
            dao = new DAO(xc);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("[action]"), Route("/Login")]
        public IActionResult Login([FromForm] Logins xlogins) 
        {
            var data = dao.login(xlogins);

            if (data == "login")
            {
                var spartans = new Tuple<List<StudentList>, List<AllStudents>, List<Subjects>>(dao.StudentList(xlogins.Id), dao.GetAllStudentx("where en.StudentId = " + xlogins.Id), dao.GetSubs());

                return View("Dashboard", spartans);
            }
            else if (data == "Admin")
            {
                var dataa = dao.GetStudents();
                return View("Adminx", dataa);
            }
            else
                return RedirectToAction("Index");
        }
      
        [HttpPost("[action]"), Route("/Novice")]
        public IActionResult Novice()
        {
            var NoviceData = dao.GetStudents();
            return View("Adminx", NoviceData);
        }

        [HttpPost("[action]"), Route("/LookThrough")]
        public IActionResult LookThrough([FromForm] GetStudents LookThoughData)
        {
            if (LookThoughData.lname == null)
            {
                return RedirectToAction("Novice");
            }
            var LookThoughList = dao.SearchThough(LookThoughData);
            return View("Adminx", LookThoughList);
        }

        [HttpPost("[action]"), Route("/AddStudents")]
        public IActionResult AddStudents(StudentDetails student)
        {
            dao.AddStudent(student);
            return RedirectToAction("SignUp");
        }

        public IActionResult Dashboard(Logins xlogins)
        {
            return View();
        }
        [HttpPost("[action]"), Route("/Adminx")]
        public IActionResult Adminx()
        {
            return View();
        }

        [HttpPost("[action]"), Route("/SignUp")]
        public IActionResult SignUp()
        {
            var datac = dao.GetCourses();
            return View(datac);
        }
        [HttpPost("[action]"), Route("/Profile")]
        public IActionResult Profile([FromForm] AllStudents students)
        {
            try
            {
                var datax = dao.GetAllStudentx("where en.StudentId = " + students.StudentId);
                if (datax.Count() != 0)
                {
                    return View("Profile", datax);
                }
                else
                    return View("ErrorTab");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost("[action]"), Route("/ErrorTab")]
        public IActionResult ErrorTab()
        {
            return View();
        }

        [HttpPost("[action]"), Route("/StudentUpdate")]
        public IActionResult StudentUpdate([FromForm] AllStudents all)
        {
            try
            {
                dao.StudUpdate(all);
                var datax = dao.GetAllStudentx("where en.StudentId = " + all.StudentId);
                //return Content(@"<script>window.close();</script>", "text/html");
                return View("Profile", datax);
            }
            catch {
                return View("Error");
            }
        }

        [HttpPost("[action]"), Route("/NotOnPressSearch")]
        public IActionResult NotOnPressSearch([FromForm] AllStudents searchStudent)
        {
            var searchData = dao.GetAllStudentx("where si.Lastname = " + searchStudent.Lastname);
            return View("Profile", searchData);
        }

        [HttpPost("[action]"), Route("/SubjectCourses")]
        public IActionResult SubjectCourses([FromForm] StairwayToCourses stc)
        {
            var tupleModel = new Tuple<List<Courses>, List<Subjects>>(dao.GetCourses(), dao.GetSubjects(stc.CourseName));
            return View(tupleModel);
        }

        [HttpPost("[action]"), Route("/PerCourses")]
        public IActionResult PerCourses([FromForm] StairwayToCourses stc)
        {
            var pc = dao.StairwayToCoursesx("where CourseName = '" + stc.CourseName + "'");
            return View("SubjectCourses", pc);
        }

        [HttpPost("[action]"), Route("/CastCourses")]
        public IActionResult CastCourses(Courses courses)
        {
            dao.AddCourses(courses);
            return RedirectToAction("SubjectCourses");
        }
        [HttpGet("[action]"), Route("/SubjectToStudent")]
        public IActionResult SubjectToStudent(AllStudents allStudents)
        {
            dao.SubjectToStudents(allStudents);
            try
            {
                var spartans = new Tuple<List<StudentList>, List<AllStudents>, List<Subjects>>(dao.StudentList(allStudents.StudentId), dao.GetAllStudentx("where en.StudentId = " + allStudents.StudentId), dao.GetSubs());
                return View("Dashboard", spartans);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost("[action]"), Route("/DeleteSubject")]
        public IActionResult DeleteSubject(AllStudents allStudents)
        {
            dao.DeleteSubs(allStudents);
            var datax = dao.GetAllStudentx("where en.StudentId = " + allStudents.StudentId);
            if (datax.Count() != 0)
            {
                return View("Profile", datax);
            }
            else
                return View("ErrorTab");
        }
        [HttpPost("[action]"), Route("/DeleteSubjectV2")]
        public IActionResult DeleteSubjectV2(AllStudents students)
        {
            dao.DeleteSubs(students);
            var spartans = new Tuple<List<StudentList>, List<AllStudents>, List<Subjects>>(dao.StudentList(students.StudentId), dao.GetAllStudentx("where en.StudentId = " + students.StudentId), dao.GetSubs());
            return View("Dashboard", spartans);
        }

        [HttpPost("[action]"), Route("/AddSubjects")]
        public IActionResult AddSubjects(Subjects sub)
        {
            dao.AddSubs(sub);
            return RedirectToAction("SubjectCourses");
        }
        [HttpPost("[action]"), Route("/EditProfile")]
        public IActionResult EditProfile(StudentDetails sd)
        {
            var data = dao.StudentList(sd.idnum);

            return View(data);
        }
        [HttpPost("[action]"), Route("/UpdateProfile")]
        public IActionResult UpdateProfile(StudentList sd)
        {
            dao.UpdateProfile(sd);
            var spartans = new Tuple<List<StudentList>, List<AllStudents>, List<Subjects>>(dao.StudentList(sd.StudentId), dao.GetAllStudentx("where en.StudentId = " + sd.StudentId), dao.GetSubs());
            return View("Dashboard", spartans);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}