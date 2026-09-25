using Coffee_Machine_Application.Model;
using Coffee_Machine_Application.Repository;

namespace Coffee_Machine_Application.Service
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        internal AuthService(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public bool IsUserExists(string userName)
        {
            return this._userRepository.IsUserExists(userName);
        }

        public async Task Register(string userName)
        {
            await this._userRepository.Add(new User(Guid.NewGuid(), userName));
        }

        public bool Login(string userName)
        {
            // Registration, Login
            if (!this.IsUserExists(userName))
            {
                return false;
            }

            User user = new User(Guid.NewGuid(), userName);
            SessionHandler.CurrentUser = user;
            return true;
        }
    }
}
