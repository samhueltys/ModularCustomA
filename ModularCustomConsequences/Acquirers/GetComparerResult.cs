using ModularSkillScripts;
using MTCustomScripts.MiscClasses;
using System;
using System.Linq;

namespace MTCustomScripts.Acquirers;

public class AcquirerGetComparerResult : IModularAcquirer
{
    public int ExecuteAcquirer(ModularSA modular, string section, string circledSection, string[] circles)
    {
        /*
         * var_1: single-unit
         * var_2: valueToCompare
         * var_3: operator
         * var_4: dataID
         * opt_5: dataSource
         */

        if (circles.Length < 4) return -1;

        if (string.IsNullOrWhiteSpace(circles[1])) return -1;

        string dataSource = (circles.Length >= 4) ? circles[4] : null;
        BattleUnitModel unit = modular.GetTargetModel(circles[0]);
        long unit_longptr = (unit == null) ? 0 : unit.Pointer.ToInt64();

        string data = Main.GetCustomMTData(unit_longptr, circles[3], dataSource);
        if (string.IsNullOrWhiteSpace(data)) return -1;

        return circles[2] switch
        {
            "=" => data.Equals(circles[1]) ? 1 : 0,
            ">" => data.Contains(circles[1]) ? 1 : 0,
            "<" => circles[1].Contains(data) ? 1 : 0,
            _ => 0
        };
    }
}