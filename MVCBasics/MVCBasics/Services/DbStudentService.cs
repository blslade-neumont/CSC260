using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class DbStudentService : DbCrudService<Student>
    {
        public DbStudentService(SchoolDbContext ctx)
            : base(ctx, ctx.Students)
        {
        }
    }
}
