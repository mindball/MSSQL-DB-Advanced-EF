using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Linq;

namespace Exercises
{
    using Data;
    using Models;
    using System.Text;

    class StartUp
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<SoftUni2EFContext> options =
                  new DbContextOptionsBuilder<SoftUni2EFContext>();
            options.UseLoggerFactory(MyLoggerFactory());

            //Task3
            //using (var context = new SoftUni2EFContext(options.Options))
            //{
            //    var result = GetEmployeesFullInformation(context);
            //    Console.WriteLine(result);
            //}

            //Task4
            using (var context = new SoftUni2EFContext(options.Options))
            {
                var result = GetEmployeesWithSalaryOver50000(context);
                Console.WriteLine(result);
            }
        }

        private static ILoggerFactory MyLoggerFactory()
        {
            ILoggerFactory myLoggerFactory
                            = LoggerFactory.Create(builder =>
                            {
                                builder
                                    .AddFilter((category, level) =>
                                        category == DbLoggerCategory.Database.Command.Name
                                        && level == LogLevel.Information)
                                    .AddConsole();
                            });

            return myLoggerFactory;
        }

        public static string GetEmployeesFullInformation(SoftUni2EFContext context)
        {
            StringBuilder sb = new StringBuilder();

            var empls = context
                .Employees
                .OrderBy(x => x.EmployeeId)
                .ToList();

            foreach (var employee in empls)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName}" +
                    $"{employee.Department} {employee.JobTitle} {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();

        }

        public static string GetEmployeesWithSalaryOver50000(SoftUni2EFContext context)
        {
            StringBuilder sb = new StringBuilder();

            var empls = context
                .Employees
                .Where(x => x.Salary > 50000)
                .OrderBy(x => x.FirstName)
                .ToList();

            foreach (var employee in empls)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();

        }
    }
}
