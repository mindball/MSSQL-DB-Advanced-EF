using System;
using System.Collections.Generic;

namespace Lesson.Data.EntitiesWithFluentApi
{
    public partial class Orders
    {
        public int Id { get; set; }
        public int? OrderNo { get; set; }
        public string OrderName { get; set; }
    }
}
