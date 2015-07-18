using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.DataProjections
{
    public static class ProjectExtensions
    {
        public static PlayerProjection AsProjected(this Player player)
        {
            return new PlayerProjection()
            {
                Age = player.Age,
                Club = player.Club.Name,
                CurrentAbility = player.CA,
                PotentialAbility = player.PA,
                DateOfBirth = player.DateOfBirth,
                FullName = string.Format("{0} {1}", player.Firstname, player.Lastname)
                //Nationality = player.Nationality.Name
            };
        }
    }
}
