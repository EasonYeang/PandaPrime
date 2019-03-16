using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TableColumn
    {
        public string title { get; set; }
        public string key { get; set; }
        public string type { get; set; }
        public int width { get; set; }
        public string align { get; set; }
    }
}
