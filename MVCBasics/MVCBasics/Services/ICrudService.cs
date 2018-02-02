using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public interface ICrudService<T>
    {
        T Create(T t);
        Task<T> CreateAsync(T t);

        IEnumerable<T> FindAll();
        Task<IEnumerable<T>> FindAllAsync();

        T Get(int id);
        Task<T> GetAsync(int id);

        T Update(T t);
        Task<T> UpdateAsync(T t);

        void Destroy(int id);
        Task DestroyAsync(int id);
    }
}
