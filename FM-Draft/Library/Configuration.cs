using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FM_Draft.Library
{
    public class Configuration
    {
        public int MaxSearchResults
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["Settings.MaxSearchResults"]);
            }
        }
    }
}
