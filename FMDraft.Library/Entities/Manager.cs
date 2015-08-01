using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Entities
{
    public class Manager
    {
        public int ID { get; set; }
        public string FullName { get; set; }
    }

    public enum ManagerMode
    {
        Player,
        CPU
    }
}
