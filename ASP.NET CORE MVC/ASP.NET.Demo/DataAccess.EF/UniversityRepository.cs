namespace DataAccess.EF
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    public class UniversityRepository<T> where T : class
    {
        UniversityContext context = new UniversityContext(null);

        protected DbSet<T> DBSet;

        public UniversityRepository()
        {
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

        public void Create(T entity)
        {
            var result = this.DBSet.Add(entity);
        }

        public void Update(T entity)
        {
            this.DBSet.Update(entity);
        }

        public void Remove(T entity)
        {
            this.DBSet.Remove(entity);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
