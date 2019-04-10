using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PPermissionDto
    {
        public string Title { get; set; }
        public int Number { get; set; }
        public int? PSN { get; set; }
        public int? Lv { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public int? Order { get; set; }
    }
}
