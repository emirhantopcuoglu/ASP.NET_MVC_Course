namespace MeetingApp.Models
{
    public static class Repository
    {
        private static List<UserInfo> _users = new List<UserInfo>();
        static Repository()
        {
            _users.Add(new UserInfo() { Id = 1, Name = "Ali", Email = "ali@mail.com", Phone = "1111", WillAttend = true });
            _users.Add(new UserInfo() { Id = 2, Name = "Ayşe", Email = "ayse@mail.com", Phone = "11122", WillAttend = true });
            _users.Add(new UserInfo() { Id = 3, Name = "Kemal", Email = "kemal@mail.com", Phone = "111133", WillAttend = true });

        }
        public static List<UserInfo> Users
        {
            get
            {
                return _users;
            }
        }

        public static void CreateUser(UserInfo user)
        {
            user.Id = Users.Count + 1 ;
            _users.Add(user);
        }

        public static UserInfo? GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
    }
}