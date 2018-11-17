using System.Collections.Generic;

namespace DataAccess.EF
{
    public interface IUniversityRepository<T> where T : class
    {
        void Create(T entity);
        List<T> GetAll();
        T GetById(int id);
        void Remove(T entity);
        void SaveChanges();
        void Update(T entity);
    }
}