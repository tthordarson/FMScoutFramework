using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_Draft.Library.Entities
{
    public class DraftCard
    {
        public Player Player { get; set; }
        public int? MaxCurrentAbility { get; set; }
        public int? MaxPotentialAbility { get; set; }
        public int ContractYears { get; set; }
        public int ContractSalary { get; set; }
    }
}
