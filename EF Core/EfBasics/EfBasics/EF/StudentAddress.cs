namespace EfBasics.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StudentAddress
    {
        public string AddressLine { get; set; }

        [Key]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
