using Lesson.Data.EntitiesWithDataAnnotations;
using Lesson.Data.EntitiesWithFluentApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Linq;

//Fluent API:
//PowerShellCommand:
//  Scaffold - DbContext - Connection "Server=.;Database=SoftUni;User=sa;Password=" - OutputDir Data/Entities - Provider Microsoft.EntityFrameworkCore.SqlServer

//Data Annotations:
//PowerShellCommand:
//  Scaffold-DbContext -Connection "Server=.;Database=SoftUni;User=sa;Password=." -OutputDir Data/Entities -Provider Microsoft.EntityFrameworkCore.SqlServer


namespace Lesson
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create logger to watch DB and write to logger(printo to console)
             ILoggerFactory MyLoggerFactory
                = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name
                            && level == LogLevel.Information)
                        .AddConsole();
                });

            //Create logger to watch DB and write to logger(prin to to Debug Output!!!)
            ILoggerFactory MyLoggerFactory2
               = LoggerFactory.Create(builder =>
               {
                   builder
                       .AddFilter((category, level) =>
                           category == DbLoggerCategory.Database.Command.Name
                           && level == LogLevel.Information)
                       .AddDebug();
               });

            DbContextOptionsBuilder<Data.EntitiesWithDataAnnotations.SoftUniContext> optionsDA =
                new DbContextOptionsBuilder<Data.EntitiesWithDataAnnotations.SoftUniContext>();

            DbContextOptionsBuilder<Data.EntitiesWithFluentApi.SoftUniContext> optionsFApi =
                new DbContextOptionsBuilder<Data.EntitiesWithFluentApi.SoftUniContext>();

            
            //Console Output
            optionsDA.UseSqlServer("Server = ; Database = SoftUni; User = ; Password = ")
                .UseLoggerFactory(MyLoggerFactory);
            
            //Console Output
            optionsFApi.UseSqlServer("Server = ; Database = SoftUni; User = sa; Password = ")
                .UseLoggerFactory(MyLoggerFactory);
            

            //Debug output
            optionsDA.UseSqlServer("Server=;Database=SoftUni;User=sa;Password=")
                .UseLoggerFactory(MyLoggerFactory2);
            //Debug output
            optionsFApi.UseSqlServer("Server = ; Database = SoftUni; User = sa; Password = ")
                .UseLoggerFactory(MyLoggerFactory2);


            using (var context =
                new Data.EntitiesWithDataAnnotations.SoftUniContext(optionsDA.Options))
            {

                var emp = context
                    .Employees
                    .FirstOrDefault();
                Console.WriteLine($"{emp.FirstName} {emp.LastName}");
            }

            //throw exeption - Lazy loading not include
            using (var context =
                new Data.EntitiesWithFluentApi.SoftUniContext(optionsFApi.Options))
            {
                var emp = context.Employees.FirstOrDefault();
                Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.Department}");
            }

            //enable Lazy loading
            //SQL Query: 
            /*
            SELECT TOP(1) [e].[EmployeeID], [e].[AddressID], [e].[DepartmentID], 
                    [e].[FirstName], [e].[HireDate], [e].[JobTitle], [e].[LastName],
                     [e].[ManagerID], [e].[MiddleName], [e].[Salary], [d].[DepartmentID],
                 [d].[ManagerID], [d].[Name]
            FROM[Employees] AS[e]
            INNER JOIN[Departments] AS[d] ON[e].[DepartmentID] = [d].[DepartmentID]
            **/

            using (var context =
                new Data.EntitiesWithDataAnnotations.SoftUniContext(optionsDA.Options))
            {

                var emp = context
                    .Employees
                    .Include(e => e.Department) //Lazy loading
                    .FirstOrDefault();
                Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.Department.Name}");
            }

            //Crud operations
            using (var context =
                new Data.EntitiesWithDataAnnotations.SoftUniContext(optionsDA.Options))
            {

                var newProject = new Data.EntitiesWithDataAnnotations.Projects()
                {
                    Description = "Research walkman",
                    Name = "Mountain crew",
                    StartDate = DateTime.Now
                };

                context.Projects.Add(newProject);
                context.SaveChanges();

                Console.WriteLine();
            }

            //Foreign key
            using (var context =
                new Data.EntitiesWithDataAnnotations.SoftUniContext(optionsDA.Options))
            {
                var address = new Data.EntitiesWithDataAnnotations.Addresses()
                {
                    AddressText = "Vitosha 12",
                    Town = new Data.EntitiesWithDataAnnotations.Towns()
                    {
                        Name = "Svoge"
                    }
                };

                context.Addresses.Add(address);
                context.SaveChanges();
            }

            //Ако сме подходили Code First -> може да се трие каскадно, но в случая сме DB First 
            //и exception throw ???!!!

        }
    }
}
