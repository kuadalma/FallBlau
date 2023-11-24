namespace app.models
{
    public class User
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string id, string name, string email, string password)
        {
            this.id = id;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
