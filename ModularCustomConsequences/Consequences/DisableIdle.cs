using ModularSkillScripts;
using System;

namespace MTCustomScripts.Consequences
{
    public class ConsequenceDisableIdle : IModularConsequence
    {
        public void ExecuteConsequence(ModularSA modular, string section, string circledSection, string[] circles)
        {      
            BattleObjectManager objManager = SingletonBehavior<BattleObjectManager>.Instance;
            // objManager.UpdatePassiveState();
            // objManager.OnRoundStart_View_AfterChoice();
            // objManager.UpdateViewState(false, false);

            foreach (BattleUnitView unitView in objManager.GetAliveViewList())
            {
                unitView.OnRoundStart_AfterChoice();
                unitView.PauseUpdate(true);
            }
        }
    }
}
