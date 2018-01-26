using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class StatisticsService
    {
        public StatisticsService(StudentService studentService, TeacherService teacherService)
        {
            this.studentService = studentService;
            this.teacherService = teacherService;
        }

        private StudentService studentService;
        private TeacherService teacherService;

        public dynamic GetStatistics()
        {
            return new Anon(new
            {
                Student = this.studentService.GetStatistics(),
                Teacher = this.teacherService.GetStatistics()
            });
        }
    }
}
