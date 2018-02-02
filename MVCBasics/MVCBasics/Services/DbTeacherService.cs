using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class DbTeacherService : DbCrudService<Teacher>
    {
        public DbTeacherService(SchoolDbContext ctx)
            : base(ctx, ctx.Teachers)
        {
        }
    }
}
