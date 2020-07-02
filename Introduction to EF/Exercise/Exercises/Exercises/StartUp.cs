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

            using (var context = new SoftUni2EFContext(options.Options))
            {
                //Task3
                //    var result = GetEmployeesFullInformation(context);               

                //Task4
                //var result = GetEmployeesWithSalaryOver50000(context);                

                //Task5
                //var result = GetEmployeesFromResearchAndDevelopment(context);

                //Task6
                var result = AddNewAddressToEmployee(context);
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
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.MiddleName,
                    x.Department,
                    x.JobTitle,
                    x.Salary,
                    x.EmployeeId
                })
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
                .Select(x => new
                {
                    x.FirstName,                   
                    x.Salary,                    
                })                
                .OrderBy(x => x.FirstName)
                .ToList();           

            foreach (var employee in empls)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();

        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUni2EFContext context)
        {
            StringBuilder sb = new StringBuilder();

            var empls = context.Employees
                .Where(x => x.Department.Name == "Research and Development")
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Salary,
                    DepartmentName = x.Department.Name
                }
                )
                .OrderBy(s => s.Salary)
                .ThenBy(s => s.FirstName);

            foreach (var employee in empls)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} " +
                    $" from {employee.DepartmentName} - '$'{employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }
        private static string AddNewAddressToEmployee(SoftUni2EFContext softUni2EFContext)
        {


            var newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            //EF know this is new address, 
            //softUni2EFContext.Add(newAddress);

            var nakov = softUni2EFContext
                .Employees
                .FirstOrDefault(x => x.LastName == "Nakov");

            //EF know this is new address
            nakov.Address = newAddress;
            softUni2EFContext.SaveChanges();

            var employess = softUni2EFContext
                .Employees
                .OrderByDescending(x => x.AddressId)
                .Select(a => a.Address.AddressText)
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employess)
            {
                sb.AppendLine($"{employee}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
