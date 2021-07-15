using System;
using System.Collections.Generic;
using System.Text;

namespace University.NetStandart.Core.Models
{
    public class Lessons:Entity
    {

        public string Name { get; set; }
        public int Point { get; set; }
        public int Credit { get; set; }
        public int StudentId { get; set; }

    }
}
