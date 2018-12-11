namespace EfBasicsCore.EF
{
    public class Exam
    {
        public int ExamId { get; set; }

        public string Name { get; set; }

        //This is need to support one to many
        public int StudentId { get; set; }

        //virtual - to support lazy loading
        //public Student Student { get; set; }
        public virtual Student Student { get; set; }
    }
}