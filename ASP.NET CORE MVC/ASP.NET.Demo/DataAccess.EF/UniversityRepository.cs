namespace DataAccess.EF
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class UniversityRepository<T> where T : class
    {
        private readonly UniversityContext context;
        protected DbSet<T> DBSet;

        public UniversityRepository(UniversityContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
            this.DBSet = this.context.Set<T>();
        }

        public List<T> GetAll()
        {
            return this.DBSet.ToList();
        }

        public T GetById(int id)
        {
            return this.DBSet.Find(id);
        }

        public T Create(T entity)
        {
            var result = this.DBSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            this.DBSet.Update(entity);
            context.SaveChanges();
        }

        public void Remove(T entity)
        {
            this.DBSet.Remove(entity);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            var entity = this.GetById(id);
            Remove(entity);
        }
    }
}
