using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayProcess.Models.Entity.Repository
{
    public interface IGenRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        T Create(T entity);
        void Update(long id,T entity);
        void Delete(long id);

    }
}