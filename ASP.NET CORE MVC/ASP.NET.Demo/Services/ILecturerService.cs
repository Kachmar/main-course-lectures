using System.Collections.Generic;
using Models.Models;

namespace Services
{
    public interface ILecturerService
    {
        void CreateLecturer(Lecturer lecturer);
        void DeleteLecturer(int id);
        List<Lecturer> GetAllLecturers();
        Lecturer GetLecturerById(int id);
        void UpdateLecturer(Lecturer lecturer);
    }
}