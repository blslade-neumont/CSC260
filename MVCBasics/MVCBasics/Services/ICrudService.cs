using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public interface ICrudService<T>
    {
        T Create(T t);
        IEnumerable<T> FindAll();
        T Get(int id);
        T Update(T t);
        void Destroy(int id);
    }
}
