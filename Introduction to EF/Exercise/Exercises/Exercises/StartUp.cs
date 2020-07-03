﻿using System;
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
                var result = GetEmployee147(context);
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
    }
}
