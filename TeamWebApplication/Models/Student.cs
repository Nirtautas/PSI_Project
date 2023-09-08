using MyWebApplication.Models;

namespace MyWebApplication.Models
{
    public class Student : User
    {
        public List<string> UploadedFiles { get; set; }
        public AcademicDegree AcademicDegree { get; set; }
        public int YearIn { get; set; }
        public Student()
        {

        }
    }
    public enum AcademicDegree
    {
        Bachelor,
        Master,
        Doctorate
    }

}
