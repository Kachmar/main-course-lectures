namespace EfCoreFluent
{
    public class Exam
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Duration { get; set; }

        public virtual Student Student { get; set; }

        public string Location { get; set; }
    }
}
