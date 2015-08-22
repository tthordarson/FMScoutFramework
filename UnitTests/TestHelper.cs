using FMScoutFramework.Core;
using FMScoutFramework.Core.Entities;
using FMScoutFramework.Core.Entities.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests
{
    public static class TestHelper
    {
        public static FMCore GetLoadedCore()
        {
            var autoResetEvent = new AutoResetEvent(false);

            var core = new FMCore(DatabaseModeEnum.Realtime);

            core.GameLoaded += () =>
            {
                autoResetEvent.Set();
            };

            core.LoadData();

            autoResetEvent.WaitOne();

            return core;
        }

        /// <summary>
        /// Converts strings into hexademical ascii representation from the football manager memory (explained here: https://github.com/ThanosSiopoudis/FMScoutFramework/issues/1,)
        /// </summary>
        /// <param name="str">string (example Chelsea)</param>
        /// <returns>ascii hexademical understood in football manager (example 43 00 68 00 65 00 6C 00 73 00 65 00 61 00)</returns>
        public static string ToFmStringHex(this string str)
        {
            var spaceString = string.Join(" ", str.ToCharArray());

            var hexValues = spaceString.ToCharArray().Select(ch =>
            {
                var hex = Convert.ToInt32(ch).ToString("X");

                if (hex == "20")
                {
                    hex = "00";
                }

                return hex;
            });

            return string.Join(" ", hexValues);
        }

        public static string GetExpectedHex(this string str)
        {
            var stringLength = str.Length;

            var hexString = str.ToFmStringHex();

            return string.Format("01 {0:X2} 00 00 {1}", stringLength, hexString);
        }
    }
}
