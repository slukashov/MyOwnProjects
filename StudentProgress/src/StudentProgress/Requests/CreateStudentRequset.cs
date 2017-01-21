namespace StudentProgress.Requests
{
    public class CreateStudentRequset
    {
        public string Name { set; get; }
        public string SecondName { set; get; }
        public string Email { set; get; }
        public long GroupId { set; get; }
    }
}
