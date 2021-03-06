﻿using FMDraft.Library;
using FMDraft.Library.Entities;
using FMDraft.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMDraft.WPF.CustomElements;

namespace FMDraft.WPF.Templates.Drafts.PlayerDraft
{
    public class PlayerDraftRoundViewModel : AbstractViewModel
    {
        private League league;

        public PlayerDraftRoundViewModel(GameCore core, League league, int roundNumber) : base(core)
        {
            RoundNumber = roundNumber;

            DraftPicks = new UpdatableObservableCollection<DraftCardViewModel>();

            this.league = league;

            InitializeDraftPicks();
        }

        private void InitializeDraftPicks()
        {
            if (IsLoaded)
            {
                DraftPicks.Clear();

                // Teams with draft cards for current round
                var teams = league.Teams.Where(x => x.DraftCards != null && x.DraftCards.ElementAtOrDefault(Index) != null);

                var draftCards = teams.Select(x =>
                {
                    var element = x.DraftCards.ElementAt(Index);
                    element.Team = x;

                    return element;
                });

                DraftPicks.AddRange(draftCards.OrderBy(x => x.Round).Select(x => 
                {
                    var vm = x.ToViewModel(core);
                    vm.PickNumber = x.Team.DraftOrder;

                    return vm;
                }));
            }
        }

        private int Index
        {
            get
            {
                return RoundNumber - 1;
            }
        }

        public int RoundNumber { get; set; }

        public string RoundHeading { get { return string.Format("Round {0}", RoundNumber); } }

        public UpdatableObservableCollection<DraftCardViewModel> DraftPicks { get; set; }
    }
}
