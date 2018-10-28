namespace Fundamentals
{
    using System;

    public class TransientService : ITransientService
    {
        public TransientService()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}