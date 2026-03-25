using HarmonyLib;
using System;
using System.Collections.Generic;
using Lethe.Patches;
using System.Linq;

namespace MTCustomScripts.Patches
{
    internal class GetSkillIdsPatch
    {
        [HarmonyPatch(typeof(UnitDataModel), nameof(UnitDataModel.GetSkillIds))]
        [HarmonyPostfix, HarmonyPriority(Priority.VeryHigh)]
        public static void UnitDataModel_GetSkillIds_Postfix(UnitDataModel __instance, ref Il2CppSystem.Collections.Generic.List<int> __result)
        {
            System.Collections.Generic.HashSet<int> removedSkillHash = Main.Instance.storedRemoveSkillHash;
            Il2CppSystem.Collections.Generic.List<int> newResult = new Il2CppSystem.Collections.Generic.List<int>();

            foreach (int id in __result)
            {
                if (!removedSkillHash.Contains(id)) newResult.Add(id);
            }

            __result = newResult;
        }

        [HarmonyPatch(typeof(UnitInformationSkillContent), nameof(UnitInformationSkillContent.SetData))]
        [HarmonyPrefix]
        public static bool UnitInformationSkillContent_SetData_Prefix(UnitInformationSkillContent __instance, UnitInformationSkillListData.SkillBoxData skillBoxData, bool updateLayoutImmediate = true)
        {
            if (Main.Instance.storedRemoveSkillHash.Contains(skillBoxData.skillModel.GetID()))
            {
                __instance.SetActive(false);
                return false;
            }
            else return true;
        }

        [HarmonyPatch(typeof(StageModel), nameof(StageModel.Init))]
        [HarmonyPrefix]
        public static void StageModel_Init_Prefix(StageStaticData stageinfo, StageModel __instance)
        {
            Main main = Main.Instance;
            if (main.changeMpDict.Count > 0) main.changeMpDict.Clear();
            if (main.storedMTDataDict.Count > 0) main.storedMTDataDict.Clear();
            if (main.storedRemoveSkillHash.Count > 0) main.storedRemoveSkillHash.Clear();
        }

        [HarmonyPatch(typeof(StageModel), nameof(StageModel.OnStageEnd))]
        [HarmonyPrefix]
        public static void StageModel_OnStageEnd_Prefix(StageModel __instance)
        {
            Main main = Main.Instance;
            if (main.changeMpDict.Count > 0) main.changeMpDict.Clear();
            if (main.storedMTDataDict.Count > 0) main.storedMTDataDict.Clear();
            if (main.storedRemoveSkillHash.Count > 0) main.storedRemoveSkillHash.Clear();
        }

        [HarmonyPatch(typeof(Data), nameof(Data.LoadCustomLocale), new[] { typeof(LOCALIZE_LANGUAGE) })]
        [HarmonyPostfix, HarmonyPriority(Priority.Normal)]
        public static void Data_LoadCustomLocale_Postfix(LOCALIZE_LANGUAGE lang)
        {
            Main main = Main.Instance;
            if (main.changeMpDict.Count > 0) main.changeMpDict.Clear();
            if (main.storedMTDataDict.Count > 0) main.storedMTDataDict.Clear();
            if (main.storedRemoveSkillHash.Count > 0) main.storedRemoveSkillHash.Clear();
        }
    }
}
