namespace Beltek.Project.Models
{
    public class Student
    {
        public int Studentid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Number { get; set; }
        public int Classid { get; set; }
        public Class Class { get; set; }
    }
}
