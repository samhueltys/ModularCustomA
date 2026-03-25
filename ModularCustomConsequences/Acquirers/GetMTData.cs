using ModularSkillScripts;
using System;
using System.Collections.Generic;

namespace MTCustomScripts.Acquirers
{
    internal class AcquirerGetMTData : IModularAcquirer
    {
        public int ExecuteAcquirer(ModularSA modular, string section, string circledSection, string[] circles)
        {
            /*
             * var_1: target
             * var_2: dataID
             * opt_3: dataSource
             * opt_5: dataType
             */

            if (circles.Length < 2) return -1;

            string dataSource = (circles.Length >= 3) ? circles[2] : null;

            BattleUnitModel unit = modular.GetTargetModel(circles[0]);
            long unit_longptr = (unit != null) ? unit.Pointer.ToInt64() : 0;

            string data = Main.GetCustomMTData(unit_longptr, circles[1], dataSource);
            if (string.IsNullOrWhiteSpace(data)) return -1;

            int dataValue = modular.GetNumFromParamString(data);
            return dataValue;
        }
    }
}
