using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure 
{
    public class UntrackedRepository : IRepository
    {
        public DateTime CreatedDate
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string WorkingDirectory
        {
            get; set;
        }

        public UntrackedRepository(string name, string workingDirectory, DateTime createdDate)
        {
            Name = name;
            WorkingDirectory = workingDirectory;
            CreatedDate = createdDate;
        }

        public TrackedRepository StartTracking()
        {
            string url = GetRemoteURL();
            TrackedRepository repo = new TrackedRepository()
                { 
                    IsActive = true,
                    WorkingDirectory = this.WorkingDirectory,
                    CreatedDate = this.CreatedDate,
                    RemoteURL = url
                };
            repo.Refresh();
            return repo;
        }

        private string GetRemoteURL()
        {
            string[] config = File.ReadAllText(WorkingDirectory + @"\.git\config").Split('\n');
            
            for (int i = 0; i < config.Length; i++)
            {
                if (config[i].Contains("url = "))
                {
                    string[] splitLine = config[i].Split(' ');
                    return splitLine[splitLine.Length - 1];
                }
            }
            return null;
            
        }
    }
}
