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
         * var_5: translationType
         * opt_6: dataSource
         */

        if (circles.Length < 4) return -1;
        Main mainClass = Main.Instance;

        string dataSource = (circles.Length >= 5) ? circles[5] : null;
        string translationType = (circles.Length >= 4) ? circles[4] : null;
        BattleUnitModel unit = modular.GetTargetModel(circles[0]);


        Type lookupType = null;
        if (circles.Length >= 5 && !string.IsNullOrEmpty(translationType)) mainClass.translatedDataTypesDict.TryGetValue(translationType, out lookupType);

        object dataValue = Main.GetCustomMTData(unit.Pointer.ToInt64(), circles[3], dataSource, lookupType);
        if (dataValue == null) return -1;


        IDataTranslation translation = mainClass.CreateTranslation(dataValue, circles[1]);
        if (translation == null) return -1;


        return circles[2] switch
        {
            "=" => translation.EqualOperator(),
            ">" => translation.SuperiorOperator(),
            "<" => translation.InferiorOperator(),
            _ => 0
        };
    }
}