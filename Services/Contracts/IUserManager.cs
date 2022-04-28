using DataAccess.Entities;

namespace Services.Contracts
{
    public interface IUserManager
    {
        User[] GetUsers();
        User[] GetUsersByFilter(string search, bool orderByDescending = false);
    }
}
