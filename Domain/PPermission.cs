using System;

namespace Domain
{
    public class PPermission
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SerialNumber { get; set; }
        public int? ParentSN { get; set; }
        public int? Level { get; set; }
        public string FilePath { get; set; }
        public string Icon { get; set; }
        public int? Order { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? LastTime { get; set; }
    }
}
