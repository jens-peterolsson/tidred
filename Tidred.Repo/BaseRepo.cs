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
        public List<T> GetAll()
        {
            using (var context = new TidredContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public void Create(T entity)
        {
            using (var context = new TidredContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (var context = new TidredContext())
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (var context = new TidredContext())
            {
                context.Set<T>().Attach(entity);

                var entry = context.Entry(entity);
                entry.State = EntityState.Modified;
                
                context.SaveChanges();
            }
        }
    }
}
