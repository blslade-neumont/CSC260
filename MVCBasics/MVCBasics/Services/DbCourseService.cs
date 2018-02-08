using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class DbCourseService : DbCrudService<Course>
    {
        public DbCourseService(SchoolDbContext ctx)
            : base(ctx, ctx.Courses)
        {
        }
    }
}
