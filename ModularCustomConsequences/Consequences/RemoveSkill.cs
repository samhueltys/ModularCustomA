using ModularSkillScripts;

namespace MTCustomScripts.Consequences
{
    public class ConsequenceRemoveSkill : IModularConsequence
    {
        public void ExecuteConsequence(ModularSA modular, string section, string circledSection, string[] circles)
        {
            /*
             * var_1: multi-target
             * var-2: multi-skill
             */

            try
            {
                Il2CppSystem.Collections.Generic.List<BattleUnitModel> unitList = modular.GetTargetModelList(circles[0]);
                if (unitList.Count <= 0) return;


                Il2CppSystem.Collections.Generic.List<SkillModel> skillList = modular.GetMultipleSkillModel(unitList, circles[1]);
                if (skillList.Count <= 0) return;

                foreach (BattleUnitModel unit in unitList)
                {
                    Il2CppSystem.Collections.Generic.List<SkillModel> removeSkillList = new Il2CppSystem.Collections.Generic.List<SkillModel>();
                    for (int i = 0; i < skillList.Count; i++)
                    {
                        UnitAttribute skillAttribute = unit.UnitDataModel._unitAttributeList.ToSystem().Find(x => x.SkillId == skillList[i].GetID());
                        if (skillAttribute != null) skillAttribute.number = 0;
                    }

                    foreach (SkillModel removeSkill in removeSkillList) skillList.Remove(removeSkill);
                    removeSkillList.Clear();
                }
            }
            catch (System.Exception ex) { Main.Logger.LogError("ConsequenceRemoveSkill error: " + ex); }
        }
    }
}
