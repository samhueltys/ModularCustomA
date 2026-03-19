using HarmonyLib;
using Lethe.Patches;
using ModularSkillScripts;
using System.Text.RegularExpressions;
using MTCustomScripts;
using System.Linq;

internal class Modular_EnactConsequence
{
    [HarmonyPatch(typeof(ModularSA), "Consequence")]
    [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
    public static bool Prefix_ModularSA_Consequence(ref string section, ModularSA __instance)
    {
        try
        {
            section = Regex.Replace(section, @"\[([^\[\]]*)\]", match =>
            {
                string matchValue = match.Groups[1].Value;

                object foundData = MTCustomScripts.Main.GetCustomMTData(__instance.modsa_unitModel.Pointer.ToInt64(), matchValue);
                return (foundData != null) ? foundData.ToString() : matchValue;
            });
        }
        catch (System.Exception ex)
        {
            MainClass.Logg.LogInfo(ex);
        }

        return true;
    }

    [HarmonyPatch(typeof(ModularSA), "ProcessBatch")]
    [HarmonyPrefix, HarmonyPriority(Priority.VeryHigh)]
    public static void Prefix_ModularSA_Consequence(string batch, ModularSA __instance)
    {
        Regex waitingReg = Main.Instance.waitingRegex;
        Match waitingMatch = waitingReg.Match(batch);

        if (waitingMatch.Success)
        {
            string waitingMatchTime = waitingMatch.Groups[1].Value;
            int finalWait = __instance.GetNumFromParamString(waitingMatchTime);


            batch = batch.Replace(waitingMatchTime + ":", "");
            System.Threading.Tasks.Task.Delay(finalWait);
        }
    }
}