using Models.Models;

namespace University.WebApi.Dto
{
    public class HomeTaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public HomeTask ToModel()
        {
            return new HomeTask() { Id = Id, Title = Title };
        }

        public static HomeTaskDto FromModel(HomeTask homeTask)
        {
            return new HomeTaskDto() { Title = homeTask.Title, Id = homeTask.Id };
        }
    }
}