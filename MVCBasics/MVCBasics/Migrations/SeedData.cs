using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MVCBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics.Migrations
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<SchoolDbContext>();
                await context.Database.EnsureCreatedAsync();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await AddRoles(roleManager);

                if (!context.Set<User>().Any()) await CreateUsers(services);

                //var userManager = services.GetRequiredService<UserManager<User>>();
                //var superMasterAdmin = await userManager.FindByIdAsync("4ad7801a-6eb4-414e-9247-1a4a971b5dc0");
                //string code = await userManager.GeneratePasswordResetTokenAsync(superMasterAdmin);
                //await userManager.ResetPasswordAsync(superMasterAdmin, code, "Test1234");
            }
        }

        private static async Task AddRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Student"))
            {
                var role = new IdentityRole();
                role.Name = "Student";
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Teacher"))
            {
                var role = new IdentityRole();
                role.Name = "Teacher";
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Registrar"))
            {
                var role = new IdentityRole();
                role.Name = "Registrar";
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task CreateUsers(IServiceProvider services)
        {
            var context = services.GetRequiredService<SchoolDbContext>();
            
            var userManager = services.GetRequiredService<UserManager<User>>();
            
            await CreateAdmins(context, userManager);
            
            await CreateRegistrars(context, userManager);

            var teacherService = services.GetRequiredService<ICrudService<Teacher>>();
            await CreateTeachers(teacherService, userManager);

            var studentService = services.GetRequiredService<ICrudService<Student>>();
            await CreateStudents(studentService, userManager);
        }

        private static async Task CreateAdmins(SchoolDbContext context, UserManager<User> userManager)
        {
            User superMasterAdmin = new User
            {
                UserName = "supermasteradmin@gmail.com",
                Email = "supermasteradmin@gmail.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(superMasterAdmin, "Test1234");
            await userManager.AddToRoleAsync(superMasterAdmin, "Admin");
        }

        private static async Task CreateRegistrars(SchoolDbContext context, UserManager<User> userManager)
        {
            User registrar1 = new User
            {
                UserName = "test-registrar@gmail.com",
                Email = "test-registrar@gmail.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(registrar1, "Test1234");
            await userManager.AddToRoleAsync(registrar1, "Registrar");
        }

        private static async Task CreateTeachers(ICrudService<Teacher> teacherService, UserManager<User> userManager)
        {
            Teacher Fahim = new Teacher()
            {
                Name = "Fahim",
                Gender = Gender.Male,
                YearsTaught = 2,
                Status = TeacherStatus.Adjunct
            };
            await teacherService.CreateAsync(Fahim);
            User teacher1 = new User
            {
                UserName = "test-teacher-1@gmail.com",
                Email = "test-teacher-1@gmail.com",
                Teacher = Fahim,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(teacher1, "Test1234");
            await userManager.AddToRoleAsync(teacher1, "Teacher");

            Teacher Beatty = new Teacher()
            {
                Name = "Beatty",
                Gender = Gender.Male,
                YearsTaught = 6,
                Status = TeacherStatus.FullTime
            };
            await teacherService.CreateAsync(Beatty);
            User teacher2 = new User
            {
                UserName = "test-teacher-2@gmail.com",
                Email = "test-teacher-2@gmail.com",
                Teacher = Beatty,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(teacher2, "Test1234");
            await userManager.AddToRoleAsync(teacher2, "Teacher");

            Teacher Lamb = new Teacher()
            {
                Name = "Lamb",
                Gender = Gender.Male,
                YearsTaught = 4,
                Status = TeacherStatus.FullTime
            };
            await teacherService.CreateAsync(Lamb);
            User teacher3 = new User
            {
                UserName = "test-teacher-3@gmail.com",
                Email = "test-teacher-3@gmail.com",
                Teacher = Lamb,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(teacher3, "Test1234");
            await userManager.AddToRoleAsync(teacher3, "Teacher");

            Teacher Ray = new Teacher()
            {
                Name = "Ray",
                Gender = Gender.Male,
                YearsTaught = 1,
                Status = TeacherStatus.FullTime
            };
            await teacherService.CreateAsync(Ray);
            User teacher4 = new User
            {
                UserName = "test-teacher-4@gmail.com",
                Email = "test-teacher-4@gmail.com",
                Teacher = Ray,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(teacher4, "Test1234");
            await userManager.AddToRoleAsync(teacher4, "Teacher");
        }

        private static async Task CreateStudents(ICrudService<Student> studentService, UserManager<User> userManager)
        {
            Student JohnDoe = new Student()
            {
                Name = "John Doe",
                Gender = Gender.Male,
                Cohort = 31,
                Degree = Degree.BSCS,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(JohnDoe);
            User student1 = new User
            {
                UserName = "test-student-1@gmail.com",
                Email = "test-student-1@gmail.com",
                Student = JohnDoe,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(student1, "Test1234");
            await userManager.AddToRoleAsync(student1, "Student");

            Student BrandonSlade = new Student()
            {
                Name = "Brandon Slade",
                Gender = Gender.Male,
                Cohort = 31,
                Degree = Degree.BSGD,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(BrandonSlade);
            User student2 = new User
            {
                UserName = "test-student-2@gmail.com",
                Email = "test-student-2@gmail.com",
                Student = BrandonSlade,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(student2, "Test1234");
            await userManager.AddToRoleAsync(student2, "Student");

            Student Justin = new Student()
            {
                Name = "Justin",
                Gender = Gender.Male,
                Cohort = 31,
                Degree = Degree.BSGD,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(Justin);
            User student3 = new User
            {
                UserName = "test-student-3@gmail.com",
                Email = "test-student-3@gmail.com",
                Student = Justin,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(student3, "Test1234");
            await userManager.AddToRoleAsync(student3, "Student");

            Student Mary = new Student()
            {
                Name = "Mary",
                Gender = Gender.Female,
                Cohort = 33,
                Degree = Degree.BSWD,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(Mary);
            User student4 = new User
            {
                UserName = "test-student-4@gmail.com",
                Email = "test-student-4@gmail.com",
                Student = Mary,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(student4, "Test1234");
            await userManager.AddToRoleAsync(student4, "Student");

            Student LazySue = new Student()
            {
                Name = "Lazy Sue",
                Gender = Gender.Female,
                Cohort = 32,
                Degree = Degree.BSTM,
                GraduationDate = new DateTime(2019, 9, 15)
            };
            await studentService.CreateAsync(LazySue);
            User student5 = new User
            {
                UserName = "test-student-5@gmail.com",
                Email = "test-student-5@gmail.com",
                Student = LazySue,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(student5, "Test1234");
            await userManager.AddToRoleAsync(student5, "Student");

            Student Damian = new Student()
            {
                Name = "Damian",
                Gender = Gender.Male,
                Cohort = 32,
                Degree = Degree.BSWD,
                GraduationDate = new DateTime(2018, 9, 15)
            };
            await studentService.CreateAsync(Damian);
            User student6 = new User
            {
                UserName = "test-student-6@gmail.com",
                Email = "test-student-6@gmail.com",
                Student = Damian,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(student6, "Test1234");
            await userManager.AddToRoleAsync(student6, "Student");

            Student Robert = new Student()
            {
                Name = "Robert",
                Gender = Gender.Male,
                Cohort = 33,
                Degree = Degree.BSCS,
                GraduationDate = new DateTime(2020, 9, 15)
            };
            await studentService.CreateAsync(Robert);
            User student7 = new User
            {
                UserName = "test-student-7@gmail.com",
                Email = "test-student-7@gmail.com",
                Student = Robert,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(student7, "Test1234");
            await userManager.AddToRoleAsync(student7, "Student");
        }
    }
}
