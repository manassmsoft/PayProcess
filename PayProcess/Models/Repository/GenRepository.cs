using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayProcess.Models.Entity.Repository
{
    public abstract class GenRepository<T>:IGenRepository<T> where T:class
    {

        public PayProcessorContext db;
        public DbSet<T> dbSet;

        public GenRepository()
        {
            db = new PayProcessorContext();
            dbSet =db.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
       public abstract T GetById(long id);

       public T Create(T entity)
       {
           var val = db.Set<T>().Add(entity);
           db.SaveChanges();
           return val;
       }
       public void Update(long id,T entity)
       {
           
           db.Entry(entity).State = EntityState.Modified;
           db.SaveChanges();
       }
        public void Delete(long id)
       {
           var entity = GetById(id);
           db.Set<T>().Remove(entity);
           db.SaveChanges();
       }

    }
}
