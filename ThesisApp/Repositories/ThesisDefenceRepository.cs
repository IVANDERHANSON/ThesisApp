using Microsoft.EntityFrameworkCore;
using ThesisApp.Data;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Repositories
{
    public class ThesisDefenceRepository : IThesisDefenceRepository
    {
        private readonly DataContext _dataContext;
        public ThesisDefenceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<ThesisDefence> GetThesisDefences()
        {
            return _dataContext.ThesisDefences.OrderBy(td => td.id).ToList();
        }

        public ThesisDefence GetThesisDefence(int id)
        {
            return _dataContext.ThesisDefences.Where(td => td.id == id).FirstOrDefault();
        }

        public bool ThesisDefenceExists(int id)
        {
            return _dataContext.ThesisDefences.Any(td => td.id == id);
        }

        public bool ThesisIdExists(int ThesisId)
        {
            return _dataContext.ThesisDefences.Any(td => td.ThesisId == ThesisId);
        }

        public bool CreateThesisDefence(ThesisDefence thesisDefence)
        {
            _dataContext.Add(thesisDefence);

            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public User GetStudent(int thesisId)
        {
            if (_dataContext.Theses.Any(t => t.id == thesisId) && !ThesisIdExists(thesisId))
            {
                return _dataContext.Users.Where(u => u.Thesis.id == thesisId).Include(u => u.PreThesis).Include(u => u.PreThesis.MentorPair).Include(u => u.Thesis).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public ICollection<User> GetExaminerLecturers(int mentorLecturerId)
        {
            return _dataContext.Users.Where(u => u.Role == "Lecturer" && mentorLecturerId != u.id && u.ThesisDefenceForExaminer.ExaminerLecturerId != u.id).OrderBy(u => u.id).ToList();
        }
    }
}
