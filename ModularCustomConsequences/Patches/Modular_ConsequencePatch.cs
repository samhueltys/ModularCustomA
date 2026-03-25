using HarmonyLib;
using Lethe.Patches;
using ModularSkillScripts;
using System.Text.RegularExpressions;
using MTCustomScripts;

internal class Modular_Consequence
{
    [HarmonyPatch(typeof(ModularSA), "Consequence")]
    [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
    public static bool Prefix_ModularSA_Consequence(ref string section, ModularSA __instance)
    {
        try
        {
            section = matchReg.Replace(section, match =>
            {
                string matchValue = match.Groups[1].Value;
                string sourceType = match.Groups[2].Success ? match.Groups[2].Value : null;

                string outValue = Main.GetCustomMTData(__instance.modsa_unitModel.Pointer.ToInt64(), match.Groups[1].Value, sourceType);
                return outValue != null ? outValue : match.Groups[0].Value;
            });
        }
        catch (System.Exception ex) { MainClass.Logg.LogInfo(ex); }

        return true;
    }

    private static readonly Regex matchReg = Main.Instance.replaceStringRegex;
}