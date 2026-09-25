namespace Coffee_Machine_Application.Repository
{
    using Coffee_Machine_Application.Model;
    using Coffee_Machine_Application.Utilities;

    public class UserRepository
    {
        private readonly string filePath = "user.json";
        private List<User> userList;
        internal UserRepository()
        {
            this.userList = new();
        }

        public async Task Add(User user)
        {
            this.userList.Add(user);
            await AsyncJsonFileHandler<User>.WriteData(filePath, userList);
        }

        public bool IsUserExists(string userName)
        {
            return userList.FindIndex((user) => user.Name == userName) != -1;
        }
    }
}
