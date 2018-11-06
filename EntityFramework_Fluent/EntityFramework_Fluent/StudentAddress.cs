namespace EntityFramework_Fluent
{
    public class StudentAddress
    {
        public string AddressLine { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
