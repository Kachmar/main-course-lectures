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

        public virtual List<Lecturer> GetAllLecturers()
        {
            return lecturerRepository.GetAll();
        }

        public virtual void UpdateLecturer(Lecturer lecturer)
        {
            this.lecturerRepository.Update(lecturer);
        }

        public virtual void DeleteLecturer(int id)
        {          
            this.lecturerRepository.Remove(id);
        }

        public virtual Lecturer GetLecturerById(int id)
        {
            return this.lecturerRepository.GetById(id);
        }

        public virtual void CreateLecturer(Lecturer lecturer)
        {
            this.lecturerRepository.Create(lecturer);
        }
    }
}
