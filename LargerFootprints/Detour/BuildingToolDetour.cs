using ColossalFramework;
using ColossalFramework.Math;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace LargerFootprints.Detour
{
    public class BuildingToolDetour
    {
        private static bool deployed = false;

        private static RedirectCallsState _BuildingTool_CheckCollidingBuildings_state;
        private static MethodInfo _BuildingTool_CheckCollidingBuildings_original;
        private static MethodInfo _BuildingTool_CheckCollidingBuildings_detour;

        public static void Deploy()
        {
            if (!deployed)
            {
                _BuildingTool_CheckCollidingBuildings_original = typeof(BuildingTool).GetMethod("CheckCollidingBuildings", BindingFlags.Static | BindingFlags.Public);
                _BuildingTool_CheckCollidingBuildings_detour = typeof(BuildingToolDetour).GetMethod("CheckCollidingBuildings", BindingFlags.Static | BindingFlags.Public);
                _BuildingTool_CheckCollidingBuildings_state = RedirectionHelper.RedirectCalls(_BuildingTool_CheckCollidingBuildings_original, _BuildingTool_CheckCollidingBuildings_detour);

                deployed = true;

                Debug.Log("LargerFootprints: BuildingTool Methods detoured!");
            }
        }

        public static void Revert()
        {
            if (deployed)
            {
                RedirectionHelper.RevertRedirect(_BuildingTool_CheckCollidingBuildings_original, _BuildingTool_CheckCollidingBuildings_state);
                _BuildingTool_CheckCollidingBuildings_original = null;
                _BuildingTool_CheckCollidingBuildings_detour = null;

                deployed = false;

                Debug.Log("LargerFootprints: BuildingTool Methods restored!");
            }
        }

        public static bool CheckCollidingBuildings(ulong[] buildingMask, ulong[] segmentMask)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            int num = buildingMask.Length;
            bool result = false;
            for (int i = 0; i < num; i++)
            {
                ulong num2 = buildingMask[i];
                if (num2 != 0uL)
                {
                    for (int j = 0; j < 64; j++)
                    {
                        if ((num2 & 1uL << j) != 0uL)
                        {
                            int num3 = i << 6 | j;
                            BuildingInfo info = instance.m_buildings.m_buffer[num3].Info;

                            if (result) Debug.Log("Checking " + info.name);

                            if ((instance.m_buildings.m_buffer[num3].m_flags & Building.Flags.Untouchable) != Building.Flags.None)
                            {
                                if ((bool)typeof(BuildingTool).GetMethod("CheckParentNode", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] {(ushort)num3, buildingMask, segmentMask}))
                                {
                                    result = true;
                                }
                            }
                            else if ((bool)typeof(BuildingTool).GetMethod("IsImportantBuilding", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] {info, (ushort)num3}))
                            {
                                result = true;
                            }

                            if (result) Debug.Log("Colliding Building: " + info.name);
                        }
                    }
                }
            }
            return result;
        }
    }
}
