using ModularSkillScripts;
using System.Collections.Generic;

namespace MTCustomScripts.Consequences;

public class ConsequenceModifySubTargetList : IModularConsequence
{
    public void ExecuteConsequence(ModularSA modular, string section, string circledSection, string[] circles)
    {
        if (modular.modsa_selfAction == null) return;
        string mode = circles[0];
        Il2CppSystem.Collections.Generic.List<BattleUnitModel> targetList = modular.GetTargetModelList(circles[1]);
        Il2CppSystem.Collections.Generic.List<BattleUnitModel> excludeList = modular.GetTargetModelList(circles[2]);

        for (int i = excludeList.Count - 1; i > -1; i--)
        {
            if (excludeList.Contains(targetList[i])) targetList.RemoveAt(i);
        }

        if (mode == "Add")
        {
            foreach(BattleUnitModel target in targetList)
            {
                foreach(SinActionModel sinActionModel in Singleton<SinManager>.Instance.GetActionListByUnit(target))
                {
                    modular.modsa_selfAction._targetDataDetail.GetCurrentTargetSet()._subTargetList.Add(new TargetSinActionData(sinActionModel));
                }
            }
        }
        else
        {
            Il2CppSystem.Collections.Generic.List<TargetSinActionData> removeSubTargetList = new Il2CppSystem.Collections.Generic.List<TargetSinActionData>();
            
            foreach(BattleUnitModel target in targetList)
            {
                foreach(TargetSinActionData TSAD in modular.modsa_selfAction._targetDataDetail.GetCurrentTargetSet()._subTargetList)
                {
                    if (TSAD._targetSinAction._unitModel.GetUnitID() == target.GetUnitID()) removeSubTargetList.Add(TSAD);
                }
                foreach(TargetSinActionData TSAD in removeSubTargetList)
                {
                    modular.modsa_selfAction._targetDataDetail.GetCurrentTargetSet()._subTargetList.Remove(TSAD);
                }
            }
        }
    }
}