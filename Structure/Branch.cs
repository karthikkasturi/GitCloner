using System;
using System.Collections.Generic;
using System.IO;

namespace Structure
{
    public class Branch
    {
        private IRepository _containerRepo;
        public Branch(IRepository repo, string Name)
        {
            this.Name = Name;
            _containerRepo = repo;
            HEAD = File.ReadAllLines(_containerRepo.WorkingDirectory + @"\.git\ref\heads\"+Name)[0];
        }
        public string HEAD { get; set; }
        public bool HasUpstream { get; set; } = true;
        public string Name { get; set; }
        public string remote { get; set; } = "origin";
        public string UpstreamHEAD { get; set; }
        public int BranchAheadBy { get; set; } = 0;
        public int BranchBehindBy { get; set; } = 0;
        public bool IsAutoPull { get; set; } = false;
        
        public BranchStatus Status { get; set; }

        internal void Check()
        {
            if (HEAD.Equals(UpstreamHEAD))
            {
                Status = BranchStatus.UptoDate;
                return;
            }
            List<string> LocalCommitList = GetCommitLog(Name);
            List<string> OriginCommitList = GetCommitLog(remote + "/" + Name);
            BranchAheadBy = LocalCommitList.Count - 1 - LocalCommitList.FindIndex((x) => x == UpstreamHEAD) ;
            BranchBehindBy = OriginCommitList.Count - OriginCommitList.FindIndex((x) => x == HEAD) - 1;
            if (BranchAheadBy == LocalCommitList.Count && BranchBehindBy == OriginCommitList.Count)
            {
                Status = BranchStatus.Deviated;
            }
            else if (BranchAheadBy < LocalCommitList.Count && BranchAheadBy > 0)
            {
                Status = BranchStatus.Ahead;
            }
            else if (BranchBehindBy < OriginCommitList.Count && BranchBehindBy > 0)
            {
                Status = BranchStatus.Behind;
            }
            else
            {
                Status = BranchStatus.UptoDate;
            }

        }

        private List<string> GetCommitLog(string name)
        {
            ProcessCaller pc = new ProcessCaller();
            var output = new StringReader(pc.GitCall(_containerRepo.WorkingDirectory ,name));
            List<string> log = new List<string>();
            while (output.Peek() != -1)
            {
                var line = output.ReadLine();
                if (line.StartsWith("commit"))
                {
                    log.Add(line.Split(' ')[1]);
                }
            }
            return log;
        }
    }
}