using ModularSkillScripts;
using System;

namespace MTCustomScripts.Consequences;

public class ConsequenceCreateUnit : IModularConsequence
{
    public void ExecuteConsequence(ModularSA modular, string section, string circledSection, string[] circles)
    {
        int unitId = modular.GetNumFromParamString(circles[0]);
        int unitLevel = modular.GetNumFromParamString(circles[1]);
        int awakenLevel = modular.GetNumFromParamString(circles[2]);
        int waveIndex = modular.GetNumFromParamString(circles[3]);

		BattleUnitModel newUnit = Singleton<BattleObjectManager>.Instance.CreateEnemyUnit(unitId, unitLevel, awakenLevel, waveIndex, unitId, null, UNIT_POSITION.MAIN);

		var aliveList = BattleObjectManager.Instance.GetAliveList(true);
		foreach (BattleUnitModel unit in aliveList) unit.RefreshSpeed();

        // int nextBattleUnitInstanceID = Singleton<BattleObjectManager>.Instance._nextBattleUnitInstanceID;
		// BattleUnitModel battleUnitModel = null;
		// UnitStaticData unitStaticData;
        // unitStaticData = Singleton<StaticDataManager>.Instance.EnemyUnitList.GetData(unitId);
        // if (unitStaticData == null)
        // {
        //     unitStaticData = Singleton<StaticDataManager>.Instance.AbnormalityUnitList.GetData(unitId);
        // }
		// if (unitStaticData is EnemyStaticData)
		// {
		// 	UnitDataModel_Enemy unitDataModel_Enemy = new UnitDataModel_Enemy(unitStaticData as EnemyStaticData);
		// 	unitDataModel_Enemy.InitData(unitStaticData, unitLevel, awakenLevel);
		// 	battleUnitModel = new BattleUnitModel_Enemy(unitDataModel_Enemy, nextBattleUnitInstanceID, waveIndex, unitId, UNIT_POSITION.MAIN);
		// }
		// if (unitStaticData is AbnormalityStaticData)
		// {
		// 	UnitDataModel_Abnormality unitDataModel_Abnormality = new UnitDataModel_Abnormality(unitStaticData as AbnormalityStaticData);
		// 	unitDataModel_Abnormality.InitData(unitStaticData, unitLevel, awakenLevel);
		// 	battleUnitModel = new BattleUnitModel_Abnormality(unitDataModel_Abnormality, nextBattleUnitInstanceID, waveIndex, unitId, UNIT_POSITION.MAIN);
		// }
		// Singleton<BattleObjectManager>.Instance.AddUnit(battleUnitModel, unitLevel, awakenLevel);
		// battleUnitModel.Init_After();
    }
}