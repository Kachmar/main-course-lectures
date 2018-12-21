using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.EF;
using Models.Models;

namespace Services
{
    public class HomeTaskService
    {
        private readonly UniversityRepository<Course> courseRepository;
        private readonly UniversityRepository<HomeTask> homeTaskRepository;

        public HomeTaskService(UniversityRepository<Course> courseRepository, UniversityRepository<HomeTask> homeTaskRepository)
        {
            this.courseRepository = courseRepository;
            this.homeTaskRepository = homeTaskRepository;
        }

        public virtual void CreateHomeTask(HomeTask homeTask, int courseId)
        {
            var course = courseRepository.GetById(courseId);
            homeTask.Course = course;
            this.homeTaskRepository.Create(homeTask);
        }

        public virtual HomeTask GetHomeTaskById(int id)
        {
            return this.homeTaskRepository.GetById(id);
        }

        public virtual void UpdateHomeTask(HomeTask homeTask)
        {
            this.homeTaskRepository.Update(homeTask);
        }

        public virtual void DeleteHomeTask(int homeTaskId)
        {
            this.homeTaskRepository.Remove(homeTaskId);
        }   
    }
}
