using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCustomScripts.MiscClasses
{
    public class MTModData(string dataID, string dataValue, string dataSource)
    {
        public string dataID = dataID;
        public string dataValue = dataValue;
        public string dataSource = (string.IsNullOrWhiteSpace(dataSource)) ? string.Empty : dataSource;
    }
}
