using System.ComponentModel.DataAnnotations;

namespace Beltek.Project.Models
{
    public class Class
    {
        public int Classid { get; set; }
        public string ClassName { get; set; }
        public int Quota { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
