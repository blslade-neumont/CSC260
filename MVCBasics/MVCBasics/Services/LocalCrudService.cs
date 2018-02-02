using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public abstract class LocalCrudService<T> : ICrudService<T>
    {
        public LocalCrudService()
        {
        }

        protected List<T> _values = new List<T>();
        private int _nextId = 1;

        //Create
        public T Create(T t)
        {
            ((dynamic)t).Id = _nextId++;
            _values.Add(t);
            return t;
        }
        public Task<T> CreateAsync(T t)
        {
            return Task.FromResult(Create(t));
        }

        //Read
        public IEnumerable<T> FindAll()
        {
            return _values.AsEnumerable();
        }
        public Task<IEnumerable<T>> FindAllAsync()
        {
            return Task.FromResult(FindAll());
        }

        public T Get(int id)
        {
            return FindAll().Where(t => ((dynamic)t).Id == id).FirstOrDefault();
        }
        public Task<T> GetAsync(int id)
        {
            return Task.FromResult(Get(id));
        }
        
        //Update
        public T Update(T t)
        {
            Destroy(((dynamic)t).Id);
            _values.Add(t);
            return t;
        }
        public Task<T> UpdateAsync(T t)
        {
            return Task.FromResult(Update(t));
        }

        //Destroy
        public void Destroy(int id)
        {
            _values.RemoveAll(inst => ((dynamic)inst).Id == id);
        }
        public Task DestroyAsync(int id)
        {
            Destroy(id);
            return Task.CompletedTask;
        }
    }
}
