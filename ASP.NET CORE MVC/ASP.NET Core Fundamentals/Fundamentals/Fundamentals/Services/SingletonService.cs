namespace Fundamentals
{
    using System;

    public class SingletonService : ISingletonService
    {
        public SingletonService()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}