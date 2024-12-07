using ThesisApp.Data;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Repositories
{
    public class PreThesisRepository : IPreThesisRepository
    {
        private readonly DataContext _dataContext;
        public PreThesisRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<PreThesis> GetPreTheses()
        {
            return _dataContext.PreTheses.OrderBy(pt => pt.id).ToList();
        }

        public PreThesis GetPreThesis(int id)
        {
            return _dataContext.PreTheses.Where(pt => pt.id == id).FirstOrDefault();
        }

        public bool PreThesisExists(int id)
        {
            return _dataContext.PreTheses.Any(pt => pt.id == id);
        }

        public bool StudentIdExists(int studentId)
        {
            return _dataContext.PreTheses.Any(pt => pt.StudentId == studentId);
        }

        public bool CreatePreThesis(PreThesis preThesis)
        {
            _dataContext.Add(preThesis);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public PreThesis GetPreThesisByStudentId(int studentId)
        {
            return _dataContext.PreTheses.Where(pt => pt.StudentId == studentId).FirstOrDefault();
        }
    }
}
