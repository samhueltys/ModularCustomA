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
<<<<<<< HEAD
=======
             * opt_5: dataType
>>>>>>> 81656175d64b8560643ab39bbcdbdb2061d2b473
             */

            if (circles.Length < 2) return -1;

<<<<<<< HEAD
            string dataSource = (circles.Length >= 3) ? circles[2] : null;
=======
            string dataSource = (circles.Length >= 4) ? circles[3] : null;
            string translationType = (circles.Length >= 3) ? circles[2] : null;
>>>>>>> 81656175d64b8560643ab39bbcdbdb2061d2b473

            BattleUnitModel unit = modular.GetTargetModel(circles[0]);
            long unit_longptr = (unit != null) ? unit.Pointer.ToInt64() : 0;

<<<<<<< HEAD
            string data = Main.GetCustomMTData(unit_longptr, circles[1], dataSource);
            if (string.IsNullOrWhiteSpace(data)) return -1;

            int dataValue = modular.GetNumFromParamString(data);
            return dataValue;
=======
            Type lookupType = null;
            if (circles.Length >= 4 && !string.IsNullOrEmpty(translationType)) Main.Instance.translatedDataTypesDict.TryGetValue(translationType, out lookupType);

            if (Main.GetCustomMTData(unit_longptr, circles[1], dataSource, lookupType) is int final) return final;
            return -1;
>>>>>>> 81656175d64b8560643ab39bbcdbdb2061d2b473
        }
    }
}
