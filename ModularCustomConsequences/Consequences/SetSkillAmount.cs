using ModularSkillScripts;
using System;

namespace MTCustomScripts.Consequences
{
    public class ConsequenceSetSkillAmount : IModularConsequence
    {
        public void ExecuteConsequence(ModularSA modular, string section, string circledSection, string[] circles)
        {
            Il2CppSystem.Collections.Generic.List<BattleUnitModel> unitList = modular.GetTargetModelList(circles[0]);
            if (unitList.Count < 1) return;
            int skillId = modular.GetNumFromParamString(circles[1]);
            int newAmount = modular.GetNumFromParamString(circles[2]);
            BattleObjectManager objManager = SingletonBehavior<BattleObjectManager>.Instance;

            foreach (BattleUnitModel unit in unitList)
            {
                foreach(UnitAttribute unitAttribute in unit.UnitDataModel._unitAttributeList)
                {
                    if (unitAttribute.skillId == skillId)
                    {
                        unitAttribute.number = newAmount;
                        // unitAttribute._isInitialized = false;
                    }
                }
                unit.UnitDataModel._level = 273;
                unit.UnitDataModel._name = ".mointpanfightmeontherooftops";
                unit.UnitDataModel._def = 1051206011;
                // objManager.GetView(unit).RefreshSkill(unit, unit.UnitDataModel.Level, unit.UnitDataModel.SyncLevel);
            }
        }
    }
}
