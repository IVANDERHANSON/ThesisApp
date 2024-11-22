using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool UserExists(int id);
    }
}
