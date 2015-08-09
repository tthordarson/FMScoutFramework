using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Drafts
{
    public static class DraftHelper
    {
        public static bool CanDraftPlayer(this DraftCard draftCard, Player player)
        {
            bool playerTooOld = draftCard.MaxAge.HasValue && player.Age > draftCard.MaxAge.Value;
            bool playerTooYoung = draftCard.MinAge.HasValue && player.Age < draftCard.MinAge.Value;

            bool playerTooHighCA = draftCard.MaxCurrentAbility.HasValue && player.CurrentAbility > draftCard.MaxCurrentAbility.Value;
            bool playerTooLowCA = draftCard.MinCurrentAbility.HasValue && player.CurrentAbility < draftCard.MinCurrentAbility.Value;

            return !(playerTooOld || playerTooYoung || playerTooHighCA || playerTooLowCA);
        }
    }
}
