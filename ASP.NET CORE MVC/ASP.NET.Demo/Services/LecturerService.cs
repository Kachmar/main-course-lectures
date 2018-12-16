using DataAccess.EF;
using Models.Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public class LecturerService
    {
        private readonly UniversityRepository<Lecturer> lecturerRepository;

        public LecturerService(UniversityRepository<Lecturer> lecturerRepository)
        {
            this.lecturerRepository = lecturerRepository;
        }

        public List<Lecturer> GetAllLecturers()
        {
            return lecturerRepository.GetAll();
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            this.lecturerRepository.Update(lecturer);
        }

        public void DeleteLecturer(int id)
        {          
            this.lecturerRepository.Remove(id);
        }

        public Lecturer GetLecturerById(int id)
        {
            return this.lecturerRepository.GetById(id);
        }

        public void CreateLecturer(Lecturer lecturer)
        {
            this.lecturerRepository.Create(lecturer);
        }
    }
}
