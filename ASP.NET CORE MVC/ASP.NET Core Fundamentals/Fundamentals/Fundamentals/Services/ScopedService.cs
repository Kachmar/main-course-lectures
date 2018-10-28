namespace Fundamentals
{
    using System;

    public class ScopedService : IScopedService
    {
        public ScopedService()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}