using ModularSkillScripts;

namespace MTCustomScripts.Acquirers
{
    public class AcquirerGetDefaultHp : IModularAcquirer
    {
        public int ExecuteAcquirer(ModularSA modular, string section, string circledSection, string[] circles)
        {
            BattleUnitModel target = modular.GetTargetModel(circles[0]);
            return target.UnitDataModel.ClassInfo.hp.defaultStat;
        }
    }
}
