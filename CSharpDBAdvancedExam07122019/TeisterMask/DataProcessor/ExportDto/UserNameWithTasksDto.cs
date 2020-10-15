namespace TeisterMask.DataProcessor.ExportDto
{
    public class UserNameWithTasksDto
    {
        public string Username { get; set; }

        public TaskDto[] Tasks { get; set; }
    }
}
