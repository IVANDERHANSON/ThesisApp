﻿using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IThesisDefenceRepository
    {
        ICollection<ThesisDefence> GetThesisDefences();
        ThesisDefence GetThesisDefence(int id);
        bool ThesisDefenceExists(int id);
        bool ThesisIdExists(int ThesisId);
        bool CreateThesisDefence(ThesisDefence thesisDefence);
        bool Save();
    }
}
