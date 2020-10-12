namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects
                .Where(t => t.Tasks.Any())
                .Select(p => new ProjectWithtTaskDto
                { 
                    Name = p.Name,
                    TaskCount = p.Tasks.Count(),
                    HasEndDate = (string.IsNullOrEmpty(p.DueDate.ToString()) ? "No" : "Yes"),
                    Tasks = p.Tasks
                            .Select(t => new TaskExportDTO 
                            { 
                                Name = t.Name,
                                Label = t.LabelType.ToString()
                            })
                            .OrderBy(t => t.Name)
                            .ToArray()
                })
                .OrderByDescending(p => p.TaskCount)
                .ThenBy(p => p.Name)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectWithtTaskDto[]),
               new XmlRootAttribute("Projects"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            StringBuilder sb = new StringBuilder();

            using (StringWriter writer = new StringWriter(sb))
            {
                xmlSerializer.Serialize(writer, projects, namespaces);
            }

            return sb.ToString().TrimEnd();
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