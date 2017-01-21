namespace StudentProgress.Requests
{
    public class CreateProfessorRequest
    {
        public string Name { set; get; }
        public string SecondName { set; get; }
        public string Email { set; get; }
        public long FacultyId { set; get; }
        public string Password { set; get; }
    }
}
