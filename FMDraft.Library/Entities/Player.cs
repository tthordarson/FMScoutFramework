﻿using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library
{
    public class Player
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Club { get; set; }
        public string League { get; set; }
        public ushort CurrentAbility { get; set; }
        public ushort PotentialAbility { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
    }
}
