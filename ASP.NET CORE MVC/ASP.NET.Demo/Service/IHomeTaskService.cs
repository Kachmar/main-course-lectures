using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    using Models.Models;

    public interface IHomeTaskService
    {
        void CreateHomeTask(HomeTask homeTask, int courseId);

        HomeTask GetHomeTaskById(int id);

        void UpdateHomeTask(HomeTask homeTaskParameter);

        void DeleteHomeTask(int homeTaskId);

        void SaveEvaluation(HomeTask homeTask);
    }

    public class HomeTaskService : IHomeTaskService
    {
        public void CreateHomeTask(HomeTask homeTask, int courseId)
        {
            throw new NotImplementedException();
        }

        public HomeTask GetHomeTaskById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateHomeTask(HomeTask homeTaskParameter)
        {
            throw new NotImplementedException();
        }

        public void DeleteHomeTask(int homeTaskId)
        {
            throw new NotImplementedException();
        }

        public void SaveEvaluation(HomeTask homeTask)
        {
            throw new NotImplementedException();
        }
    }
}
