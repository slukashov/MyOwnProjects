using System.Collections.Generic;

namespace StudentProgress.Entities
{
  
    public class Student 
    {
        public long Id { set; get; }
        public long? AccountId { set; get; }
        public Account Account { set; get; }
        public Group Group { set; get; }
        public long? GroupId { set; get; }
        public virtual ICollection<LessonStudent> ListOfLessonss { set; get; }
        public virtual ICollection<Mark> ListOfMarks { set; get; }
        public virtual ICollection<FinalMark> ListOfFinalMarks { set; get; }

        protected Student()
        { }

        public Student(Account account, Group group)
        {
            Account = account;
            Group = group;
        }
    }
}
