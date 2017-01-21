namespace StudentProgress.Entities
{
    public class Account
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string SerName { set; get;}
        public string Email { set; get; }
        public string Password { set; get; }

        public Account() { }

        public Account(string name, string serName, string email, string password)
        {
            Name = name;
            SerName = serName;
            Email = email;
            Password = password;
        }
    }
}
