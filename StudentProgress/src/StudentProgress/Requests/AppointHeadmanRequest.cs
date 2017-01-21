namespace StudentProgress.Requests
{
    public class AppointHeadmanRequest
    {
        public string Password { set; get; }
        public long StudentId { set; get; }
        public long GroupId { set; get; }
    }
}
