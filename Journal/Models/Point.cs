using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.Models
{
    public class Point
    {
        public int Id { get; set; }
        public int StudentPoint { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
