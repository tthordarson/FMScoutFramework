﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMScoutFramework.Core;
using FMScoutFramework.Core.Entities;
using FMDraft.Library.Entities;

namespace FMDraft.Library
{
    public class GameCore
    {
        private FMCore core;
        private bool isLoaded;

        public GameState GameState { get; set; }
        public QueryService QueryService { get; set; }

        public Action LoadFailedCallback = () => { };
        public Action LoadCompleteCallback = () => {};

        public bool IsLoaded { get { return this.isLoaded; } }

        public GameCore()
        {
            this.isLoaded = false;
            this.core = new FMCore(DatabaseModeEnum.Realtime);
            this.QueryService = new QueryService(this.core);
            this.core.GameLoaded += this.OnLoaded;
        }

        private void OnLoaded()
        {
            this.isLoaded = true;
        }

        public void Load()
        {
            try
            {
                this.core.LoadData();
                LoadCompleteCallback();

                if (GameState == null)
                {
                    GameState = new GameState();
                }
            }
            catch
            {
                this.isLoaded = false;
                LoadFailedCallback();
            }
        }
    }
}
