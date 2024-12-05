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

        public bool PreThesisIdExists(int preThesisId)
        {
            return _dataContext.MentorPairs.Any(mp => mp.PreThesisId == preThesisId);
        }

        public User GetStudent(int preThesisId)
        {
            return _dataContext.Users.Where(u => u.PreThesis.id == preThesisId).Include(u => u.PreThesis).FirstOrDefault();
        }

        public ICollection<User> GetMentorLecturers()
        {
            return _dataContext.Users.Where(u => u.Role == "Lecturer" && u.MentorPair.MentorLecturerId != u.id).OrderBy(u => u.id).ToList();
        }

        public bool MentorLecturerIdExists(int mentorLecturerId)
        {
            return _dataContext.MentorPairs.Any(mp => mp.MentorLecturerId == mentorLecturerId);
        }

        public bool CreateMentorPair(MentorPair mentorPair)
        {
            _dataContext.Add(mentorPair);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public User GetStudentForEditMentorPair(int studentId)
        {
            return _dataContext.Users.Where(u => u.id == studentId).Include(u => u.PreThesis).Include(u => u.PreThesis.MentorPair).FirstOrDefault();
        }

        public bool UpdateMentorPair(MentorPair oldMentorPair, MentorPair mentorPair)
        {
            oldMentorPair.MentorLecturerId = mentorPair.MentorLecturerId;
            return Save();
        }

        public bool SameMentorLecturerId(MentorPair oldMentorPair, MentorPair mentorPair)
        {
            oldMentorPair.MentorLecturerId = mentorPair.MentorLecturerId;
            _dataContext.SaveChanges();
            return true;
        }
    }
}
