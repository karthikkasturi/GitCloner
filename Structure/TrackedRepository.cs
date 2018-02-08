using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Structure
{
    public class TrackedRepository : IRepository
    {
        public string RemoteURL { get; set; }
        public string Name { get; set; }
        public string WorkingDirectory { get; set; }
        public DateTime CreatedDate { set; get; }
        public DateTime LastModified { get; set; }
        public DateTime RecentCheck { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<string, Branch> Branches { get; set; } =  new Dictionary<string, Branch>();
        public string CurrentBranch { get; set; }
        public void Refresh()
        {
            if (!ConfirmExists())
            {
                //TODO Throw an exception that says that the file is no longer available
            }
            if (!ConfirmIsRepository())
            {
                //TODO Throw and exception that says that the file isnt a valid git repo
            }
            Fetch();
            SetCurrentBranch();
            RefreshBranches();
            RecentCheck = DateTime.Now;
        }

        private void SetCurrentBranch()
        {
            //git branch
            //return starred branch
            ProcessCaller pc = new ProcessCaller();
            string[] branchOp = pc.GitCall(WorkingDirectory, "branch").Split('\n');
            foreach (string line in branchOp)
            {
                if (line.Contains("*"))
                {
                    CurrentBranch = line.Split(' ')[1];
                }
            }
            
        }
    

        public void UpdateAutos()
        {
            string initBranch = CurrentBranch;
            ProcessCaller pc = new ProcessCaller();
            foreach (Branch branch in Branches.Values)
            {
                if (branch.IsAutoPull)
                {
                    if (branch.Status == BranchStatus.Behind)
                    {
                        Checkout(branch.Name);
                        if(CurrentBranch.Equals(branch.Name))
                            branch.MergeOrigin();
                    }
                }
            }
            Checkout(initBranch);
        }

        private void Checkout(string branchName)
        {
            ProcessCaller pc = new ProcessCaller();
            pc.GitCall(WorkingDirectory, $"checkout {branchName}");
            SetCurrentBranch();
        }

        private void RefreshBranches()
        {
            IEnumerable<string> localBranches = ReadConfigForBranches();
            
            Dictionary<string, string> originHeads = ReadFETCH_HEAD();
            
            foreach (string branchName in localBranches)
            {
                if (!Branches.Keys.Contains(branchName))
                {
                    Branches.Add( branchName, new Branch(this, branchName) );
                }
                Branch updatingBranch = Branches[branchName];
                if (updatingBranch.HasUpstream)
                {
                    updatingBranch.UpstreamHEAD = originHeads[branchName];
                    updatingBranch.Check();
                }
            }
        }

        private IEnumerable<string> ReadConfigForBranches()
        {
            string[] config = File.ReadAllText(WorkingDirectory + @"\.git\config").Split('\n');
            List<string> branches = new List<string>();
            for (int i = 0; i < config.Length; i++)
            {
                if (config[i].StartsWith("[branch \""))
                {
                    string branchName;
                    branchName = config[i].Substring(9);
                    branchName = branchName.Substring(0, branchName.Length-2);
                    branches.Add(branchName);
                }
            }
            return branches;
        }

        private Dictionary<string, string> ReadFETCH_HEAD()
        {
            Dictionary<string, string> originHeads = new Dictionary<string, string>();
            string[] lines = File.ReadAllText(WorkingDirectory + @"\.git\FETCH_HEAD").Split('\n');
            foreach (string line in lines)
            {
                if (line.Contains("branch"))
                {
                    string[] splittedLine = line.Split(' ');
                    int splitLen = splittedLine.Length;
                    string branchName = splittedLine[splitLen - 3];
                    branchName = branchName.Substring(1);
                    branchName = branchName.Substring(0, branchName.Length - 1);
                    originHeads.Add(branchName, splittedLine[0]);
                }
            }
            return originHeads;
        }

        private void Fetch()
        {
            ProcessCaller pc = new ProcessCaller();
            pc.GitCall(WorkingDirectory, "fetch");
        }

        private bool ConfirmIsRepository()
        {
            if (Directory.Exists(WorkingDirectory+@"\.git"))
            {
                return true;
            }
            return false;
        }

        private bool ConfirmExists()
        {
            if (Directory.Exists(WorkingDirectory))
            {
                return true;
            }
            return false;
        }
    }
}
