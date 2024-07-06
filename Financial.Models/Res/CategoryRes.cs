using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Models.Responses
{
    public class CategoryRes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public bool SystemDefault { get; set; }

        public int Type { get; set; }
    }
}
