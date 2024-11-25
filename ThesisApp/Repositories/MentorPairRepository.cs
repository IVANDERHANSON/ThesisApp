using Microsoft.EntityFrameworkCore;
using ThesisApp.Data;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Repositories
{
    public class MentorPairRepository : IMentorPairRepository
    {
        private readonly DataContext _dataContext;
        public MentorPairRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<MentorPair> GetMentorPairs()
        {
            return _dataContext.MentorPairs.OrderBy(mp => mp.id).ToList();
        }

        public MentorPair GetMentorPair(int id)
        {
            return _dataContext.MentorPairs.Where(mp => mp.id == id).FirstOrDefault();
        }

        public bool MentorPairExists(int id)
        {
            return _dataContext.MentorPairs.Any(mp => mp.id == id);
        }

        public User GetStudent(int preThesisId)
        {
            if (_dataContext.PreTheses.Any(t => t.id == preThesisId) && !_dataContext.MentorPairs.Any(mp => mp.PreThesisId == preThesisId))
            {
                return _dataContext.Users.Where(u => u.PreThesis.id == preThesisId).Include(u => u.PreThesis).FirstOrDefault();
            } else
            {
                return null;
            }
        }

        public ICollection<User> GetMentorLecturers()
        {
            return _dataContext.Users.Where(u => u.Role == "Lecturer" && u.MentorPair.MentorLecturerId != u.id).OrderBy(u => u.id).ToList();
        }
    }
}
