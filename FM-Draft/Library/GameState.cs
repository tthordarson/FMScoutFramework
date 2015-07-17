using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMScoutFramework.Core;
using FMScoutFramework.Core.Entities;

namespace FM_Draft.Library
{
    public class GameState
    {
        private FMCore core;

        public Action LoadFailedCallback = () => { };
        public Action LoadCompleteCallback = () => {};

        public GameState()
        {
            this.core = new FMCore(DatabaseModeEnum.Realtime);
            this.core.GameLoaded += this.OnLoaded;
        }

        private void OnLoaded()
        {
            
        }

        public void Load()
        {
            try
            {
                this.core.LoadData();
                LoadCompleteCallback();
            }
            catch
            {
                LoadFailedCallback();
            }
        }
    }
}
