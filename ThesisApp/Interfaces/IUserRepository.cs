﻿using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool UserExists(int id);
        ICollection<User> GetStudents();
        ICollection<User> GetLecturers();
        User GetUserForStudentDashboard(int studentId);
        ICollection<User> GetStudentsForLecturerDashboard(int lecturerId);
    }
}
