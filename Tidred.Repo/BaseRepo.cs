using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Repo
{
    public abstract class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected TidredContext Context = new TidredContext();

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Set<T>().Attach(entity);

            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;

            Context.SaveChanges();
        }
    }
}
