using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IRepository
    {
        string Name { get; set; }
        string WorkingDirectory { get; set; }
        DateTime CreatedDate { set; get; }
    }
}
