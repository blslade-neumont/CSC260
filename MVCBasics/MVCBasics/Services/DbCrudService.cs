using Microsoft.EntityFrameworkCore;
using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class DbCrudService<T> : ICrudService<T>
        where T : class, new()
    {
        public DbCrudService(DbContext ctx, DbSet<T> db)
        {
            this.ctx = ctx;
            this.db = db;
        }

        private DbContext ctx;
        private DbSet<T> db;

        //Create
        public T Create(T t)
        {
            var result = db.Add(t);
            ctx.SaveChanges();
            return result.Entity;
        }
        public async Task<T> CreateAsync(T t)
        {
            var result = await db.AddAsync(t);
            await ctx.SaveChangesAsync();
            return result.Entity;
        }

        //Read
        public IEnumerable<T> FindAll()
        {
            return db.ToArray().AsEnumerable();
        }
        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return (await db.ToArrayAsync()).AsEnumerable();
        }

        public T Get(int id)
        {
            return db.Find(id);
        }
        public async Task<T> GetAsync(int id)
        {
            return await db.FindAsync(id);
        }

        //Update
        public T Update(T t)
        {
            var result = db.Update(t);
            ctx.SaveChanges();
            return result.Entity;
        }
        public async Task<T> UpdateAsync(T t)
        {
            var result = db.Update(t);
            await ctx.SaveChangesAsync();
            return result.Entity;
        }

        //Destroy
        public void Destroy(int id)
        {
            var found = Get(id);
            if (found == null) return;
            db.Remove(found);
            ctx.SaveChanges();
        }
        public async Task DestroyAsync(int id)
        {
            var found = await GetAsync(id);
            if (found == null) return;
            db.Remove(found);
            await ctx.SaveChangesAsync();
        }
    }
}
