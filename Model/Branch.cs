using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Branch
    {
        public int Id;
        public int ContainerRepoId { get; set; }
        public string HEAD { get; set; }
        public bool HasUpstream { get; set; } = true;
        public string Name { get; set; }
        public string remote { get; set; } = "origin";
        public DateTime UpstreamLastCommitTime { get; set; }
        public DateTime LocalLastCommitTime { get; set; }
        public string UpstreamHEAD { get; set; }
        public int BranchAheadBy { get; set; } = 0;
        public int BranchBehindBy { get; set; } = 0;
        public bool IsAutoPull { get; set; } = false;
        public BranchStatus Status { get; set; }
        
        //Should we have an IsNotify Bool property here for the
        //desktop notification config or should we continue with
        //the DesktopNotif class which has only one prop?
    }
}
