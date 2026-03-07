using ModularSkillScripts;
using System;

namespace MTCustomScripts.Consequences
{
    public class ConsequenceAddEgo : IModularConsequence
    {
        public void ExecuteConsequence(ModularSA modular, string section, string circledSection, string[] circles)
        {
            var targetList = modular.GetTargetModelList(circles[0]);
            int egoId = modular.GetNumFromParamString(circles[1]);
            int uptie = modular.GetNumFromParamString(circles[2]);
            foreach(BattleUnitModel target in targetList)
            {
                UNIT_FACTION faction = target.Faction;
                EgoStaticData egoStaticData = Singleton<StaticDataManager>.Instance.EgoList.GetData(egoId);
                BattleEgoModel battleEgoModel = new BattleEgoModel(faction, egoStaticData, uptie);
                target._erosionData._egoList.Add(battleEgoModel);

                // int awkId = battleEgoModel.GetAwakeningSkillID();
                // int corsId = battleEgoModel.GetCorrosionSkillID();

                // SkillStaticDataList skillList = Singleton<StaticDataManager>.Instance._skillList;
                // SkillStaticData data = skillList.GetData(awkId);
                
                // UnitAttribute skillAttribute = new UnitAttribute();
                
                // skillAttribute.number = 0;
                // skillAttribute.skillId = awkId;
                
                // SkillModel skillModel = new SkillModel(data, target.UnitDataModel.Level, target.UnitDataModel.SyncLevel);
                
                // skillModel._skillData._skillEgoType = 0;
                // // if (circles.Length > 5) skillModel.skillData.canDuel = false;

                // target.UnitDataModel._unitAttributeList.Add(skillAttribute);
                // target.UnitDataModel._skillList.Add(skillModel);
                // SingletonBehavior<BattleObjectManager>.Instance.GetView(target).AddSkill(skillModel);


                // if (corsId != 0)
                // {
                //     SkillStaticData data2 = skillList.GetData(corsId);
                //     UnitAttribute skillAttribute2 = new UnitAttribute();
                //     skillAttribute2.number = 0;
                //     skillAttribute2.skillId = corsId;
                //     SkillModel skillModel2 = new SkillModel(data2, target.UnitDataModel.Level, target.UnitDataModel.SyncLevel);
                //     skillModel2._skillData._skillEgoType = 0;
                //     target.UnitDataModel._unitAttributeList.Add(skillAttribute2);
                //     target.UnitDataModel._skillList.Add(skillModel2);
                //   SingletonBehavior<BattleObjectManager>.Instance.GetView(target).AddSkill(skillModel2);
                // }
            }
        }
    }
}
