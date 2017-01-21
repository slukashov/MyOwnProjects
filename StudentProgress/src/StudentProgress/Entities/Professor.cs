namespace StudentProgress.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("professor")]
    public class Professor 
    {
        public long Id { set; get; }
        public long? AccountId { set; get; }
        public long? FacultyId { set; get; }
        public Account Account { set; get; }
        public Faculty Faculty { set; get; }

        public Professor(Account account, Faculty faculty)
        {
            Faculty = faculty;
            Account = account;
        }

        protected Professor() { FacultyId = null; }

       
    }
}
