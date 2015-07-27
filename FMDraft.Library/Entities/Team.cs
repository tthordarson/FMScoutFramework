﻿using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Entities
{
    public class Team
    {
        public string Name { get; set; }
        public City City { get; set; }
        public int Reputation { get; set; }
        public IEnumerable<DraftCard> DraftCards { get; set; }

        public Team()
        {
            this.DraftCards = new List<DraftCard>();
        }
    }
}