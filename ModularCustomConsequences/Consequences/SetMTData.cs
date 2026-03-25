using DG.Tweening;
using ModularSkillScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCustomScripts.Consequences
{
    internal class ConsequenceSetMTData : IModularConsequence
    {
        public void ExecuteConsequence(ModularSA modular, string section, string circledSection, string[] circles)
        {
            /*
             * var_1: target
             * var_2: dataID
             * var_3: dataValue
             * opt_4: dataSource
             * opt_5: dataType
             */

            if (circles.Length < 3) return;
            if (string.IsNullOrEmpty(circles[1]) || string.IsNullOrEmpty(circles[2])) return;

            string dataSource = (circles.Length >= 3) ? circles[2] : null;

            BattleUnitModel unit = modular.GetTargetModel(circles[0]);
            long unit_longptr = (unit != null) ? unit.Pointer.ToInt64() : 0;

            Main.SetCustomMTData(unit_longptr, circles[1], circles[2], dataSource);
        }
    }
}
