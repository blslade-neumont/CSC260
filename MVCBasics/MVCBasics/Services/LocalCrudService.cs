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

        //Read
        public IEnumerable<T> FindAll()
        {
            return _values.AsEnumerable();
        }

        public T Get(int id)
        {
            return FindAll().Where(t => ((dynamic)t).Id == id).FirstOrDefault();
        }
        
        //Update
        public T Update(T t)
        {
            Destroy(((dynamic)t).Id);
            _values.Add(t);
            return t;
        }

        //Destroy
        public void Destroy(int id)
        {
            _values.RemoveAll(inst => ((dynamic)inst).Id == id);
        }
    }
}
