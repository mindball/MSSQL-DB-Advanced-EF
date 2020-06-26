using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson.Data.EntitiesWithDataAnnotations
{
    public partial class Items
    {
        [Key]
        public int Id { get; set; }
        public int? OrderNumber { get; set; }
        [StringLength(50)]
        public string ItemName { get; set; }
        [StringLength(50)]
        public string OrderName { get; set; }
    }
}
