namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            throw new NotImplementedException();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var users = context.Employees
                 .Where(t => t.EmployeesTasks.Any())
                 .Select(u => new UserNameWithTasksDto
                 { 
                     Username = u.Username,
                     Tasks = u.EmployeesTasks 
                            .Where(p => p.Task.OpenDate >= date)
                            .Select(ta => new TaskDto
                            { 
                                TaskName = ta.Task.Name,
                                OpenDate = ta.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                                DueDate = ta.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                                LabelType = ta.Task.LabelType.ToString(),
                                ExecutionType = ta.Task.ExecutionType.ToString()
                            })
                            .OrderByDescending(x => DateTime.ParseExact(x.DueDate, @"d", CultureInfo.InvariantCulture))
                            .ThenBy(x => x.TaskName)
                            .ToArray()
                            
                 })
                 .OrderByDescending(x => x.Tasks.Count())
                 .ThenBy(u => u.Username)
                 .Take(10)
                 .ToArray();

            string result = JsonConvert.SerializeObject(users, Formatting.Indented);

            return result;
        }
    }
}