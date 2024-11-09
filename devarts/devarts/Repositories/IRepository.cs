using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Repositories
{
    public interface IRepository<T>
    {
        void Add(T element);

        void Delete(T element);

        void SaveChanges();
    }
}