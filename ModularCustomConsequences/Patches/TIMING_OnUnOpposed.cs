using HarmonyLib;
using ModularSkillScripts;
using ModularSkillScripts.Patches;

namespace MTCustomScripts.Patches
{
    internal class OnUnOpposed
    {
        [HarmonyPatch(typeof(BattleUnitModel), nameof(BattleUnitModel.OnTakeAttackDamage))]
        [HarmonyPostfix, HarmonyPriority(Priority.VeryHigh)]
        public static void Postfix_BattleUnitModel_OnTakeAttackDamage(BattleActionModel action, CoinModel coin, int realDmg, int hpDamage, BATTLE_EVENT_TIMING timing, bool isCritical, BattleUnitModel __instance)
        {
            if (action == null) return;

            BattleActionModelManager mgr = Singleton<BattleActionModelManager>.Instance;
            if (mgr == null) return;
            if (mgr.IsDuel(action)) return;

            int actevent = MainClass.timingDict["OnUnOpposed"];
            BattleUnitModel attacker = action.Model;

            foreach (PassiveModel passiveModel in __instance._passiveDetail.PassiveList)
            {
                if (!passiveModel.CheckActiveCondition()) continue;
                long passiveModel_intlong = passiveModel.Pointer.ToInt64();
                if (!SkillScriptInitPatch.modpaDict.ContainsKey(passiveModel_intlong)) continue;

                foreach (ModularSA modpa in SkillScriptInitPatch.modpaDict[passiveModel_intlong])
                {
                    modpa.modsa_passiveModel = passiveModel;
                    modpa.modsa_coinModel = coin;
                    modpa.modsa_killerModel = attacker;
                    modpa.lastFinalDmg = realDmg;
                    modpa.lastHpDmg = hpDamage;
                    modpa.wasCrit = isCritical;
                    modpa.modsa_target_list.Clear();
                    modpa.modsa_target_list.Add(__instance);
                    modpa.Enact(__instance, null, null, action, actevent, timing);
                }
            }

            foreach (PassiveModel passiveModel in __instance._passiveDetail.EgoPassiveList)
            {
                if (!passiveModel.CheckActiveCondition()) continue;
                long passiveModel_intlong = passiveModel.Pointer.ToInt64();
                if (!SkillScriptInitPatch.modpaDict.ContainsKey(passiveModel_intlong)) continue;

                foreach (ModularSA modpa in SkillScriptInitPatch.modpaDict[passiveModel_intlong])
                {
                    modpa.modsa_passiveModel = passiveModel;
                    modpa.modsa_coinModel = coin;
                    modpa.modsa_killerModel = attacker;
                    modpa.lastFinalDmg = realDmg;
                    modpa.lastHpDmg = hpDamage;
                    modpa.wasCrit = isCritical;
                    modpa.modsa_target_list.Clear();
                    modpa.modsa_target_list.Add(__instance);
                    modpa.Enact(__instance, null, null, action, actevent, timing);
                }
            }

            foreach (BuffModel buffModel in __instance._buffDetail.GetActivatedBuffModelAll())
            {
                long buffmodel_intlong = buffModel.Pointer.ToInt64();
                if (!SkillScriptInitPatch.modbaDict.ContainsKey(buffmodel_intlong)) continue;

                foreach (ModularSA modba in SkillScriptInitPatch.modbaDict[buffmodel_intlong])
                {
                    modba.modsa_buffModel = buffModel;
                    modba.modsa_coinModel = coin;
                    modba.modsa_killerModel = attacker;
                    modba.lastFinalDmg = realDmg;
                    modba.lastHpDmg = hpDamage;
                    modba.wasCrit = isCritical;
                    modba.modsa_target_list.Clear();
                    modba.modsa_target_list.Add(__instance);
                    modba.Enact(__instance, null, null, action, actevent, timing);
                }
            }
        }
    }
}
