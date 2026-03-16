using System;
using System.Collections.Generic;
using HarmonyLib;

namespace MTCustomScripts.Patches
{
    public class Test_Patch
    {
        [HarmonyPatch(typeof(SystemAbilityDetail), nameof(SystemAbilityDetail.AddAbilityThisRound))]
        [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix_SystemAbilityDetail_AddAbilityThisRound(int unitInstanceID, SYSTEM_ABILITY_KEYWORD newKeyword, int stack, int turn, SystemAbilityDetail __instance, ref SystemAbility __result)
        {
            if (!CustomSystemAbilities_Main.CheckOverwriteAbility(newKeyword, out CustomSystemAbility customAbility)) return true;
            customAbility.SetStack(stack, turn);
            customAbility.Init(unitInstanceID);
            __instance._activatedAbilityList.Add(customAbility);
            __result = customAbility;
            return false;
        }

        [HarmonyPatch(typeof(SystemAbilityDetail), nameof(SystemAbilityDetail.AddAbilityNextRound))]
        [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix_SystemAbilityDetail_AddAbilityNextRound(int unitInstanceID, SYSTEM_ABILITY_KEYWORD newKeyword, int stack, int turn, SystemAbilityDetail __instance, ref SystemAbility __result)
        {
            if (!CustomSystemAbilities_Main.CheckOverwriteAbility(newKeyword, out CustomSystemAbility customAbility)) return true;
            customAbility.SetStack(stack, turn);
            customAbility.Init(unitInstanceID);
            __instance._nextTurnAbilityList.Add(customAbility);
            __result = customAbility;
            return false;
        }

        [HarmonyPatch(typeof(SystemAbilityDetail), nameof(SystemAbilityDetail.CreateNewSystemAbility))]
        [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix_SystemAbilityDetail_CreateNewSystemAbility(SYSTEM_ABILITY_KEYWORD keyword, int stack, int turn, SystemAbilityDetail __instance, ref SystemAbility __result)
        {
            if (!CustomSystemAbilities_Main.CheckOverwriteAbility(keyword, out CustomSystemAbility customAbility)) return true;
            customAbility.SetStack(stack, turn);
            __result = customAbility;
            return false;
        }

        [HarmonyPatch(typeof(SystemAbilityDetail), nameof(SystemAbilityDetail.DestoryAbility))]
        [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix_SystemAbilityDetail_DestoryAbility(SYSTEM_ABILITY_KEYWORD keyword, SystemAbilityDetail __instance)
        {
            if (!CustomSystemAbilities_Main.CheckOverwriteAbility(keyword, out CustomSystemAbility customAbility)) return true;
                SystemAbility currentActiveCustom = __instance._activatedAbilityList.ToSystem().Find(x => (x as CustomSystemAbility).GetCustomIdentifier() == customAbility.GetCustomIdentifier());
                if (currentActiveCustom != null)
                {
                    currentActiveCustom.Destroy();
                    __instance._activatedAbilityList.Remove(currentActiveCustom);
                }

                SystemAbility currentNextCustom = __instance._nextTurnAbilityList.ToSystem().Find(x => (x as CustomSystemAbility).GetCustomIdentifier() == customAbility.GetCustomIdentifier());
                if (currentNextCustom != null)
                {
                    currentNextCustom.Destroy();
                    __instance._nextTurnAbilityList.Remove(currentNextCustom);
                }
            return false;
        }

        [HarmonyPatch(typeof(SystemAbilityDetail), nameof(SystemAbilityDetail.DestroyAbility))]
        [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix_SystemAbilityDetail_DestroyAbility(SYSTEM_ABILITY_KEYWORD keyword, SystemAbilityDetail __instance)
        {
            if (!CustomSystemAbilities_Main.CheckOverwriteAbility(keyword, out CustomSystemAbility customAbility)) return true;
                SystemAbility currentActiveCustom = __instance._activatedAbilityList.ToSystem().Find(x => (x as CustomSystemAbility).GetCustomIdentifier() == customAbility.GetCustomIdentifier());
                if (currentActiveCustom != null)
                {
                    currentActiveCustom.Destroy();
                    __instance._activatedAbilityList.Remove(currentActiveCustom);
                }

                SystemAbility currentNextCustom = __instance._nextTurnAbilityList.ToSystem().Find(x => (x as CustomSystemAbility).GetCustomIdentifier() == customAbility.GetCustomIdentifier());
                if (currentNextCustom != null)
                {
                    currentNextCustom.Destroy();
                    __instance._nextTurnAbilityList.Remove(currentNextCustom);
                }
            return false;
        }

        /*
        [HarmonyPatch(typeof(SystemAbilityDetail), nameof(SystemAbilityDetail.FindOrAddAbilityThisRound))]
        [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix_SystemAbilityDetail_FindOrAddAbilityThisRound(int instanceID, SYSTEM_ABILITY_KEYWORD newKeyword, int stack, int turn, SystemAbilityDetail __instance, ref SystemAbility __result)
        {
            if (!CustomSystemAbilities_Main.CheckOverwriteAbility(newKeyword, out CustomSystemAbility customAbility)) return true;
            SystemAbility currentActiveCustom = __instance._activatedAbilityList.ToSystem().Find(x => (x as CustomSystemAbility).GetCustomIdentifier() == customAbility.GetCustomIdentifier());
            if (currentActiveCustom != null) __result = currentActiveCustom;
            else
            {
                customAbility.SetStack(stack, turn);
                customAbility.Init(instanceID);
                __instance._activatedAbilityList.Add(customAbility);
                __result = customAbility;
            }
            return false;
        }

        [HarmonyPatch(typeof(SystemAbilityDetail), nameof(SystemAbilityDetail.FindOrAddAbilityNextRound))]
        [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix_SystemAbilityDetail_FindOrAddAbilityNextRound(int instanceID, SYSTEM_ABILITY_KEYWORD newKeyword, int stack, int turn, SystemAbilityDetail __instance, ref SystemAbility __result)
        {
            if (!CustomSystemAbilities_Main.CheckOverwriteAbility(newKeyword, out CustomSystemAbility customAbility)) return true;
            SystemAbility nextTurnCustom = __instance._nextTurnAbilityList.ToSystem().Find(x => (x as CustomSystemAbility).GetCustomIdentifier() == customAbility.GetCustomIdentifier());
            if (nextTurnCustom != null) __result = nextTurnCustom;
            else
            {
                customAbility.SetStack(stack, turn);
                customAbility.Init(instanceID);
                __instance._nextTurnAbilityList.Add(customAbility);
                __result = customAbility;
            }
            return false;
        }


        [HarmonyPatch(typeof(SystemAbilityDetail), nameof(SystemAbilityDetail.HasAbility))]
        [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix_SystemAbilityDetail_HasAbility(SYSTEM_ABILITY_KEYWORD keyword, SystemAbilityDetail __instance, ref bool __result)
        {
            if (!CustomSystemAbilities_Main.CheckOverwriteAbility(keyword, out CustomSystemAbility customAbility)) return true;
            SystemAbility currentActiveCustom = __instance._activatedAbilityList.ToSystem().Find(x => (x as CustomSystemAbility).GetCustomIdentifier() == customAbility.GetCustomIdentifier());
            if (currentActiveCustom != null) __result = true;
            else
            {
                SystemAbility nextTurnCustom = __instance._nextTurnAbilityList.ToSystem().Find(x => (x as CustomSystemAbility).GetCustomIdentifier() == customAbility.GetCustomIdentifier());
                if (nextTurnCustom != null) __result = true;
                else return true;
            }

            return false;
        }
        */

        /*
        [HarmonyPatch(typeof(SystemAbilityDetail), nameof(SystemAbilityDetail.GetType))]
        [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix_SystemAbilityDetail_GetType(SYSTEM_ABILITY_KEYWORD keyword, SystemAbilityDetail __instance, ref Il2CppSystem.Type __result)
        {
            if (!CustomSystemAbilities_Main.CheckOverwriteAbility(keyword, out CustomSystemAbility customAbility)) return true;
            __result = customAbility.GetIl2CppType();
            return false;
        }
        */

        public static Il2CppSystem.Type SystemAbilityKeywordEnumType = Il2CppSystem.Type.GetType(nameof(SYSTEM_ABILITY_KEYWORD), true);
    }
}
