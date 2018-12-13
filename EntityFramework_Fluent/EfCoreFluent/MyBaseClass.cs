namespace EfCoreFluent
{
    public abstract class MyBaseClass
    {
        public int Id { get; set; }
        public string PropertyBase { get; set; }
    }

    public class MyClassA : MyBaseClass
    {
        public string PropertyA { get; set; }
    }

    public class MyClassB : MyBaseClass
    {
        public string PropertyB { get; set; }
    }
}
