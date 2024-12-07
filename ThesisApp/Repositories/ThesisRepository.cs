using ThesisApp.Data;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Repositories
{
    public class ThesisRepository : IThesisRepository
    {
        private readonly DataContext _dataContext;
        public ThesisRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Thesis> GetTheses()
        {
            return _dataContext.Theses.OrderBy(t => t.id).ToList();
        }

        public Thesis GetThesis(int id)
        {
            return _dataContext.Theses.Where(t => t.id == id).FirstOrDefault();
        }

        public bool ThesisExists(int id)
        {
            return _dataContext.Theses.Any(t => t.id == id);
        }

        public bool StudentIdExists(int studentId)
        {
            return _dataContext.Theses.Any(t => t.StudentId == studentId);
        }

        public bool CreateThesis(Thesis thesis)
        {
            _dataContext.Add(thesis);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
