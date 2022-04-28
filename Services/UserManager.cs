using DataAccess;
using DataAccess.Entities;
using DataAccess.Repository;
using Services.Contracts;

namespace Services
{
    public class UserManager : IUserManager
    {
        UserRepository _userRepository;

        public UserManager(DBContext dBContext)
        {
            _userRepository = dBContext.UserRepository();
        }

        public User[] GetUsers()
        {
            return _userRepository.GetAll();
        }

        public User[] GetUsersByFilter(string search, bool orderByDescending = false)
        {
            return _userRepository.GetBySearchFilter(search, orderByDescending);
        }
    }
}
