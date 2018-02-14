using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UntrackedRepository : IRepository
    {
        public int Id
        {
            get; set;
        }
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
    }
}