using System;
using System.Collections.Generic;

namespace Lesson.Test.Models
{
    public partial class Items
    {
        public int Id { get; set; }
        public int? OrderNumber { get; set; }
        public string ItemName { get; set; }
        public string OrderName { get; set; }
    }
}
