using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TrackedRepository : IRepository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WorkingDirectory { get; set; }
        public string RemoteURL { get; set; }
        public DateTime CreatedDate { set; get; }
        public DateTime LastModified { get; set; }
        public DateTime RecentCheck { get; set; }
        public bool IsActive { get; set; }
        public List<int> BranchIds { get; set; } = new List<int>();
        public string CurrentBranch { get; set; }
    }
}