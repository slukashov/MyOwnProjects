using System.Collections.Generic;

namespace StudentProgress.Entities
{
    public class Faculty
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public virtual ICollection<Professor> ListOfProfessors { set; get; }

        public Faculty()
        {
            ListOfProfessors = new List<Professor>();
        }

        public Faculty(string name):this()
        {
            Name = name;
        }
    }
}