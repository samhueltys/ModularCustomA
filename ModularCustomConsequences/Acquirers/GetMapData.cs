using ModularSkillScripts;

namespace MTCustomScripts.Acquirers;

public class AcquirerGetMapData : IModularAcquirer
{
    public int ExecuteAcquirer(ModularSA modular, string section, string circledSection, string[] circles)
    {
        /*
        * var_1: current/mapName
        * var_2: size/active/id
        */

        BattleMapPreset selectedMap = null;
        if (circles[0] == "current") selectedMap = BattleMapManager.Instance._mapObject._currentMap;
        else BattleMapManager.Instance._mapObject._mapDict.TryGetValue(circles[0], out selectedMap);

        if (selectedMap == null) return -1;

        switch (circles[1])
        {
            default: return -1;
            case "size": return (int)selectedMap.GetMapSize();
            case "active": return selectedMap.IsActive() ? 1 : 0;
            case "id":
<<<<<<< HEAD
                Main.SetCustomMTData(modular.modsa_unitModel.Pointer.ToInt64(), circles[2], selectedMap.GetMapID(), "GetMapData");
=======
                Main.SetCustomMTData(modular.modsa_unitModel.Pointer.ToInt64(), circles[2], selectedMap.GetMapID(), "GetMapData", typeof(string));
>>>>>>> 81656175d64b8560643ab39bbcdbdb2061d2b473
                return 1;
        }
    }
}