using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Entities
{
    public class DraftCard
    {
        public Player Player { get; set; }
        public int Round { get; set; }
        public int? MaxCurrentAbility { get; set; }
        public int? MinCurrentAbility { get; set; }
        public int? MaxPotentialAbility { get; set; }
        public int? MinPotentialAbility { get; set; }
        public int? MaxAge { get; set; }
        public int? MinAge { get; set; }
        public int ContractYears { get; set; }
        public int ContractSalary { get; set; }
    }
}
