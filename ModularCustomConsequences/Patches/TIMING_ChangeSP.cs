using HarmonyLib;
using ModularSkillScripts;
using ModularSkillScripts.Patches;
using System;
using System.Collections.Generic;

namespace MTCustomScripts.Patches
{
    internal class ChangeSP
    {
        [HarmonyPatch(typeof(BattleUnitModel), nameof(BattleUnitModel.OnChangeMp))]
        [HarmonyPostfix, HarmonyPriority(Priority.VeryHigh)]
        public static void Postfix_BattleUnitModel_OnChangeMP(int oldMp, int newMp, BattleUnitModel __instance)
        {
            Main.Instance.changeMpDict[__instance] = new int[2] { oldMp, newMp };

            int actevent = MainClass.timingDict["OnChangeSP"];
            int actevent_other = MainClass.timingDict["OnOtherChangeSP"];

            StyxUtils.GenericModularPatches(__instance, actevent, actevent_other, BATTLE_EVENT_TIMING.ALL_TIMING);
        }

        [HarmonyPatch(typeof(BattleUnitModel), nameof(BattleUnitModel.OnTakeMpDamage))]
        [HarmonyPostfix, HarmonyPriority(Priority.VeryHigh)]
        public static void Postfix_BattleUnitModel_OnTakeMpDamage(BattleUnitModel __instance, BattleUnitModel attacker, int value, BATTLE_EVENT_TIMING timing, DAMAGE_SOURCE_TYPE sourceType, BASE_MENTAL_CONDITION mentalConditionOrNone, BattleActionModel actionOrNull, BUFF_UNIQUE_KEYWORD buffKeyword = BUFF_UNIQUE_KEYWORD.None)
        {
            int actevent = MainClass.timingDict["OnTakeSPDamage"];
            int actevent_other = MainClass.timingDict["OnOtherTakeSPDamage"];

            foreach (PassiveModel passiveModel in __instance._passiveDetail.PassiveList)
            {
                if (!passiveModel.CheckActiveCondition()) continue;
                long passiveModel_intlong = passiveModel.Pointer.ToInt64();
                if (!SkillScriptInitPatch.modpaDict.ContainsKey(passiveModel_intlong)) continue;

                foreach (ModularSA modpa in SkillScriptInitPatch.modpaDict[passiveModel_intlong])
                {
                    modpa.modsa_passiveModel = passiveModel;
                    modpa.changedamage_source = sourceType;
                    modpa.modsa_killerModel = attacker;
                    modpa.Enact(__instance, null, null, actionOrNull, actevent, timing);
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
                    modpa.changedamage_source = sourceType;
                    modpa.modsa_killerModel = attacker;
                    modpa.Enact(__instance, null, null, actionOrNull, actevent, timing);
                }
            }

            foreach (BuffModel buffModel in __instance._buffDetail.GetActivatedBuffModelAll())
            {
                long buffmodel_intlong = buffModel.Pointer.ToInt64();
                if (!SkillScriptInitPatch.modbaDict.ContainsKey(buffmodel_intlong)) continue;

                foreach (ModularSA modba in SkillScriptInitPatch.modbaDict[buffmodel_intlong])
                {
                    modba.modsa_buffModel = buffModel;
                    modba.changedamage_source = sourceType;
                    modba.modsa_killerModel = attacker;
                    modba.Enact(__instance, null, null, actionOrNull, actevent, timing);
                }
            }

            if (actevent_other == 0) return;

            BattleObjectManager battleObjManager_inst = SingletonBehavior<BattleObjectManager>.Instance;
            foreach (BattleUnitModel unit in battleObjManager_inst.GetAliveListExceptSelf(__instance, false, false))
            {
                foreach (PassiveModel passiveModel in unit._passiveDetail.PassiveList)
                {
                    if (!passiveModel.CheckActiveCondition()) continue;
                    long passiveModel_intlong = passiveModel.Pointer.ToInt64();
                    if (!SkillScriptInitPatch.modpaDict.ContainsKey(passiveModel_intlong)) continue;

                    foreach (ModularSA modpa in SkillScriptInitPatch.modpaDict[passiveModel_intlong])
                    {
                        modpa.modsa_passiveModel = passiveModel;
                        modpa.changedamage_source = sourceType;
                        modpa.modsa_killerModel = attacker;
                        modpa.modsa_victimModel = __instance;
                        modpa.Enact(__instance, null, null, actionOrNull, actevent, timing);
                    }
                }
                foreach (PassiveModel passiveModel in unit._passiveDetail.EgoPassiveList)
                {
                    if (!passiveModel.CheckActiveCondition()) continue;
                    long passiveModel_intlong = passiveModel.Pointer.ToInt64();
                    if (!SkillScriptInitPatch.modpaDict.ContainsKey(passiveModel_intlong)) continue;

                    foreach (ModularSA modpa in SkillScriptInitPatch.modpaDict[passiveModel_intlong])
                    {
                        modpa.modsa_passiveModel = passiveModel;
                        modpa.changedamage_source = sourceType;
                        modpa.modsa_killerModel = attacker;
                        modpa.modsa_victimModel = __instance;
                        modpa.Enact(__instance, null, null, actionOrNull, actevent, timing);
                    }
                }
                foreach (BuffModel buffModel in unit._buffDetail.GetActivatedBuffModelAll())
                {
                    long buffmodel_intlong = buffModel.Pointer.ToInt64();
                    if (!SkillScriptInitPatch.modbaDict.ContainsKey(buffmodel_intlong)) continue;

                    foreach (ModularSA modba in SkillScriptInitPatch.modbaDict[buffmodel_intlong])
                    {
                        modba.modsa_buffModel = buffModel;
                        modba.changedamage_source = sourceType;
                        modba.modsa_killerModel = attacker;
                        modba.modsa_victimModel = __instance;
                        modba.Enact(__instance, null, null, actionOrNull, actevent, timing);
                    }
                }
            }
        }
    }
}
