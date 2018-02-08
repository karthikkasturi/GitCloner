using System;
using System.Collections.Generic;
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

        //public TrackedRepository StartTracking()
        //{
        //    return new TrackedRepository();
        //}
    }
}
