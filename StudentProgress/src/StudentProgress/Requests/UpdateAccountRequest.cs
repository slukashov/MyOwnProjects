namespace StudentProgress.Requests
{
    public class UpdateAccountRequest
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string SerName { get; set; }
    }
}
