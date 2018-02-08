using System;

namespace Structure
{
    public interface IRepository
    {
        string Name { get; set; }
        string WorkingDirectory { get; set; }
        DateTime CreatedDate { set; get; }
    }
}