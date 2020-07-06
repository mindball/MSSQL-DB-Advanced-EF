using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Linq;

namespace Exercises
{
    using Data;
    using Models;
    using System.Globalization;
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
                //var result = AddNewAddressToEmployee(context);

                //Task7
                //var result = GetEmployeesInPeriod(context);
                //Console.WriteLine(result);

                //Task8
                //var result = GetAddressesByTown(context);
                //Console.WriteLine(result);

                //Task9
                //var result = GetEmployee147(context);
                //Console.WriteLine(result);

                //Task10
                //var result = GetDepartmentsWithMoreThan5Employees(context);
                //Console.WriteLine(result);

                //Task11
                //var result = GetLatestProjects(context);
                //Console.WriteLine(result);

                //Task12
                //var result = IncreaseSalaries(context);
                //Console.WriteLine(result);

                //Task13
                //var result = FindEmployeesByFirstNameStartinWithSa(context);
                //Console.WriteLine(result);

                var result = DeleteProjectById(context);
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

        public static string GetEmployeesInPeriod(SoftUni2EFContext context)
        {
            var empls = context.Employees
                .Where(p => p.EmployeesProjects
                            .Any(s => s.Project.StartDate.Year >= 2001 &&
                            s.Project.StartDate.Year <= 2001))
                .Select(x => new
                {
                    EmployeeFullName = x.FirstName + " " + x.LastName,
                    ManagerFullName = x.Manager.FirstName + " " + x.Manager.LastName,
                    Projects = x.EmployeesProjects.Select(p => new
                    {
                        ProjectName = p.Project.Name,
                        StartDate = p.Project.StartDate,
                        EndDate = p.Project.EndDate
                    }).ToList()
                })
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in empls)
            {
                sb.AppendLine($"{employee.EmployeeFullName} - Manager: {employee.ManagerFullName}");
                foreach (var project in employee.Projects)
                {
                    var startDateFormat = project.StartDate.ToString("M/d/yyyy h:mm:ss tt",
                        CultureInfo.InvariantCulture);
                    var endDateFormat =
                                project.EndDate.HasValue ?
                                project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt",
                                CultureInfo.InvariantCulture) :
                                "not finished";


                    sb.AppendLine($"--{project.ProjectName} - {startDateFormat} - {endDateFormat}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUni2EFContext context)
        {
            var addresses = context.Addresses
                .Include(e => e.Employees)
                .Include(t => t.Town)
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(b => b.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count} employees");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUni2EFContext context)
        {

            var emp = context.Employees
                .Where(a => a.EmployeeId == 147)
                .Select(b => new
                {
                    FullName = b.FirstName + " " + b.LastName,
                    b.JobTitle,
                    Projects = b.EmployeesProjects
                    .Select(ep => ep.Project.Name)
                    .OrderBy(pe => pe)
                    .ToList()
                }).First();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{emp.FullName} - {emp.JobTitle}");

            foreach (var project in emp.Projects)
            {
                sb.AppendLine($"{project.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUni2EFContext context)
        {
            var departments = context.Departments
                .Where(e => e.Employees.Count > 5)
                .OrderBy(s => s.Employees.Count)
                .ThenBy(v => v.Name)
                .Select(x => new
                {
                    DepartmentName = x.Name,
                    ManagerFullName = x.Manager.FirstName + " " + x.Manager.LastName,
                    Employees = x.Employees.Select(e => new
                    {
                        EmployeeFirstName = e.FirstName,
                        EmployeeLastName = e.LastName,
                        EmployeeJobNam = e.JobTitle
                    })
                    .OrderBy(e => e.EmployeeFirstName)
                    .ThenBy(e => e.EmployeeLastName)
                    .ToList()
                })
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.DepartmentName} - {department.ManagerFullName}");

                foreach (var emp in department.Employees)
                {
                    sb.AppendLine($"{emp.EmployeeFirstName} {emp.EmployeeLastName} - " +
                        $"{emp.EmployeeJobNam}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUni2EFContext context)
        {
            var projects = context.Projects
                    .OrderByDescending(a => a.StartDate)
                    .Take(10)
                    .Select(b => new
                    {
                        b.Name,
                        b.Description,
                        StartDateProject = b.StartDate.ToString("M/d/yyyy h:mm:ss tt",
                            CultureInfo.InvariantCulture)
                    })
                    .OrderBy(c => c.Name)
                    .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var project in projects)
            {
                sb.AppendLine($"{project.Name}");
                sb.AppendLine($"{project.Description}");
                sb.AppendLine($"{project.StartDateProject}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUni2EFContext context)
        {
            context.Employees
                .Where(x => new[] {"Engineering",
                        "Tool Design",
                        "Marketing",
                        "Information Services"}
                        .Contains(x.Department.Name))
                .ToList()
                .ForEach(e => e.Salary *= 1.12m);

            context.SaveChanges();

            StringBuilder sb = new StringBuilder();

            var empl = context.Employees
                .Where(x => x.Department.Name == "Engineering"
                        || x.Department.Name == "Tool Design"
                        || x.Department.Name == "Marketing"
                        || x.Department.Name == "Information Services")
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            foreach (var employee in empl)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} " +
                    $"({employee.Salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string FindEmployeesByFirstNameStartinWithSa(SoftUni2EFContext context)
        {
            var emp = context.Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        e.Salary
                    })
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var e in emp)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUni2EFContext context)
        {
            var project = context.Projects.FirstOrDefault(p => p.ProjectId == 2);

            var projEmployees = context.EmployeesProjects
                .Where(ep => ep.ProjectId == 2)
                .ToList();

            context.EmployeesProjects.RemoveRange(projEmployees);
            context.Projects.Remove(project);

            context.SaveChanges();

            var projects = context.Projects
                .Select(x => x.Name)
                .Take(10)
                .ToList();


            StringBuilder sb = new StringBuilder();

            foreach (var p in projects)
            {
                sb.AppendLine(p);
            }

            return sb.ToString().TrimEnd();
        }


    }
}
