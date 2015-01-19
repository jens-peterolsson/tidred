using System;
namespace Tidred.Repo
{
    public interface IBaseRepo<T> where T : class
    {
        void Create(T entity);
        void Delete(T entity);
        System.Collections.Generic.List<T> GetAll();
        void Update(T entity);
    }
}
