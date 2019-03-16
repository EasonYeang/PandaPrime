using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PPermissionVm
    {
        public int SerialNumber { get; set; }
        public int? ParentSN { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string FilePath { get; set; }
        public List<PPermissionVm> Child { get; set; }
    }
}
