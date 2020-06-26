using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson.Data.EntitiesWithDataAnnotations
{
    [Table("Deleted_Employees")]
    public partial class DeletedEmployees
    {
        [Key]
        public int EmployeeId { get; set; }
        [StringLength(30)]
        public string FirstName { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }
        [StringLength(30)]
        public string MiddleName { get; set; }
        [StringLength(30)]
        public string JobTitle { get; set; }
        public int? DepartmentId { get; set; }
        [Column(TypeName = "decimal(15, 2)")]
        public decimal? Salary { get; set; }
    }
}
