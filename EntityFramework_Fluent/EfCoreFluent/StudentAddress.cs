namespace EfCoreFluent
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class StudentAddress
    {
        public string AddressLine { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
