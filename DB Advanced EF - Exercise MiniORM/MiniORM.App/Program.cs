namespace MiniORM.App
{
    using System.Linq;
    using Data;
    using Data.Entities;

    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = 
                @"Server=;Database=MiniOrm;User=sa;Password=";
            var context = new SoftUniDbContext(connectionString);

            context.Employees.Add(new Employee
            {
                FirstName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true,
            });

            var employee = context.Employees.Last();
            employee.FirstName = "Modified";

            context.SaveChanges();
        }
    }
}
