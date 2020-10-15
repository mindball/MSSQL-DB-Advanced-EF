using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeisterMask.DataProcessor.ImportDto
{
    class EmployeeTaskDto
    {
        [Required]
        [RegularExpression("^[A-Za-z0-9]+")]
        [StringLength(40, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^(.+)@(.+)$")]
        public string Email { get; set; }

        [Required] 
        [RegularExpression(@"\d{3}-\d{3}-\d{4}")]
        public string Phone { get; set; }

        public int[] Tasks { get; set; }
    }
}
