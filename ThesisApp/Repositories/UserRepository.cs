using Microsoft.EntityFrameworkCore;
using ThesisApp.Data;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<User> GetUsers()
        {
            return _dataContext.Users.OrderBy(u => u.id).ToList();
        }

        public User GetUser(int id)
        {
            return _dataContext.Users.Where(u => u.id == id).FirstOrDefault();
        }

        public bool UserExists(int id)
        {
            return _dataContext.Users.Any(u => u.id == id);
        }

        public ICollection<User> GetStudents()
        {
            return _dataContext.Users.Where(u => u.Role == "Student").Include(u => u.PreThesis).Include(u => u.PreThesis.MentorPair).Include(u => u.PreThesis.MentorPair.MentoringSessions).Include(u => u.Thesis).Include(u => u.Thesis.ThesisDefence).OrderBy(u => u.id).ToList();
        }

        public ICollection<User> GetLecturers()
        {
            return _dataContext.Users.Where(u => u.Role == "Lecturer").OrderBy(u => u.id).ToList();
        }

        public User GetUserForStudentDashboard(int id)
        {
            return _dataContext.Users.Where(u => u.id == id).Include(u => u.PreThesis).Include(u => u.PreThesis.MentorPair).Include(u => u.PreThesis.MentorPair.MentoringSessions).Include(u => u.Thesis).Include(u => u.Thesis.ThesisDefence).FirstOrDefault();
        }
    }
}
