namespace Coffee_Machine_Application.Repository
{
    using Coffee_Machine_Application.Model;
    public class UserRepository
    {
        private List<User> userList;
        internal UserRepository()
        {
            this.userList = new();
        }

        public void Add(User user)
        {
            this.userList.Add(user);
        }

        public bool IsUserExists(string userName)
        {
            return userList.FindIndex((user) => user.Name == userName) != -1;
        }
    }
}
