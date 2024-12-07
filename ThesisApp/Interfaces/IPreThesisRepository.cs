using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IPreThesisRepository
    {
        ICollection<PreThesis> GetPreTheses();
        PreThesis GetPreThesis(int id);
        bool PreThesisExists(int id);
        bool StudentIdExists(int studentId);
        bool CreatePreThesis(PreThesis preThesis);
        bool Save();
        PreThesis GetPreThesisByStudentId(int studentId);
    }
}
