using HarmonyLib;
using MTCustomScripts;
using System;
using Il2CppInterop.Runtime.Injection;
internal class StyxPatch
{
    //Add to "load()", ClassInjector.RegisterTypeInIl2Cpp<SkillAbility_Test>();

    // [HarmonyPatch]
    // public class StagePatch
    // {
    //     [HarmonyPatch(typeof(SkillModel), nameof(SkillModel.Init), new Type[] { })]
    //     [HarmonyPostfix]
    //     static void Postfix_Stage(SkillModel __instance)
    //     {
    //         try
    //         {
    //             __instance.SkillAbilityList.Add(new SkillAbility_Test());
    //         }
    //         catch (Exception ex)
    //         {
    //             Main.Logger?.LogError("Postfix_Stage exception: " + ex);
    //         }
    //     }
    // }

    // public class SkillAbility_Test : SkillAbility
    // {
    //         public SkillAbility_Test (IntPtr ptr) : base(ptr) { }

    //         public unsafe SkillAbility_Test () : base(ClassInjector.DerivedConstructorPointer<SkillAbility_Test>())
    //         {
    //             ClassInjector.DerivedConstructorBody(this);
    //         }

    //         public override bool IsShow()
    //         {
    //             Main.Logger.LogInfo("IsShow on the dogshit ability triggered, rejoice");
    //             return true;
    //         }
    // }

    
}