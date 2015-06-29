using ColossalFramework;
using ColossalFramework.Math;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace LargerFootprints.Detour
{
    public class BuildingManagerDetour
    {
        private static bool deployed = false;

        private static RedirectCallsState _BuildingManager_CreateBuilding_state;
        private static MethodInfo _BuildingManager_CreateBuilding_original;
        private static MethodInfo _BuildingManager_CreateBuilding_detour;

        public static void Deploy()
        {
            if (!deployed)
            {
                _BuildingManager_CreateBuilding_original = typeof(BuildingManager).GetMethod("CreateBuilding", BindingFlags.Instance | BindingFlags.Public);
                _BuildingManager_CreateBuilding_detour = typeof(BuildingManagerDetour).GetMethod("CreateBuilding", BindingFlags.Instance | BindingFlags.Public);
                _BuildingManager_CreateBuilding_state = RedirectionHelper.RedirectCalls(_BuildingManager_CreateBuilding_original, _BuildingManager_CreateBuilding_detour);

                deployed = true;

                Debug.Log("LargerFootprints: BuildingManager Methods detoured!");
            }
        }

        public static void Revert()
        {
            if (deployed)
            {
                RedirectionHelper.RevertRedirect(_BuildingManager_CreateBuilding_original, _BuildingManager_CreateBuilding_state);
                _BuildingManager_CreateBuilding_original = null;
                _BuildingManager_CreateBuilding_detour = null;

                deployed = false;

                Debug.Log("LargerFootprints: BuildingManager Methods restored!");
            }
        }

        public bool CreateBuilding(out ushort building, ref Randomizer randomizer, BuildingInfo info, Vector3 position, float angle, int length, uint buildIndex)
        {
            var _m_buildings = Singleton<BuildingManager>.instance.m_buildings;

            ushort num;
            if (_m_buildings.CreateItem(out num, ref randomizer))
            {
                if (length <= 0)
                {
                    length = info.m_cellLength;
                }
                building = num;
                _m_buildings.m_buffer[(int)building].m_flags = Building.Flags.Created;
                _m_buildings.m_buffer[(int)building].Info = info;
                // begin mod
                _m_buildings.m_buffer[(int)building].m_width = (byte)info.m_cellWidth;
                _m_buildings.m_buffer[(int)building].m_length = (byte)length;
                // end mod
                _m_buildings.m_buffer[(int)building].m_frame0 = default(Building.Frame);
                _m_buildings.m_buffer[(int)building].m_buildIndex = buildIndex;
                _m_buildings.m_buffer[(int)building].m_angle = angle;
                _m_buildings.m_buffer[(int)building].m_position = position;
                _m_buildings.m_buffer[(int)building].m_baseHeight = 0;
                _m_buildings.m_buffer[(int)building].m_ownVehicles = 0;
                _m_buildings.m_buffer[(int)building].m_guestVehicles = 0;
                _m_buildings.m_buffer[(int)building].m_sourceCitizens = 0;
                _m_buildings.m_buffer[(int)building].m_targetCitizens = 0;
                _m_buildings.m_buffer[(int)building].m_citizenUnits = 0u;
                _m_buildings.m_buffer[(int)building].m_netNode = 0;
                _m_buildings.m_buffer[(int)building].m_subBuilding = 0;
                _m_buildings.m_buffer[(int)building].m_waterSource = 0;
                _m_buildings.m_buffer[(int)building].m_nextGridBuilding = 0;
                _m_buildings.m_buffer[(int)building].m_nextGridBuilding2 = 0;
                _m_buildings.m_buffer[(int)building].m_electricityBuffer = 0;
                _m_buildings.m_buffer[(int)building].m_waterBuffer = 0;
                _m_buildings.m_buffer[(int)building].m_sewageBuffer = 0;
                _m_buildings.m_buffer[(int)building].m_garbageBuffer = 0;
                _m_buildings.m_buffer[(int)building].m_crimeBuffer = 0;
                _m_buildings.m_buffer[(int)building].m_customBuffer1 = 0;
                _m_buildings.m_buffer[(int)building].m_customBuffer2 = 0;
                _m_buildings.m_buffer[(int)building].m_productionRate = 0;
                _m_buildings.m_buffer[(int)building].m_waterPollution = 0;
                _m_buildings.m_buffer[(int)building].m_fireIntensity = 0;
                _m_buildings.m_buffer[(int)building].m_problems = Notification.Problem.None;
                _m_buildings.m_buffer[(int)building].m_lastFrame = 0;
                _m_buildings.m_buffer[(int)building].m_tempImport = 0;
                _m_buildings.m_buffer[(int)building].m_tempExport = 0;
                _m_buildings.m_buffer[(int)building].m_finalImport = 0;
                _m_buildings.m_buffer[(int)building].m_finalExport = 0;
                _m_buildings.m_buffer[(int)building].m_waterSource = 0;
                _m_buildings.m_buffer[(int)building].m_education1 = 0;
                _m_buildings.m_buffer[(int)building].m_education2 = 0;
                _m_buildings.m_buffer[(int)building].m_education3 = 0;
                _m_buildings.m_buffer[(int)building].m_teens = 0;
                _m_buildings.m_buffer[(int)building].m_youngs = 0;
                _m_buildings.m_buffer[(int)building].m_adults = 0;
                _m_buildings.m_buffer[(int)building].m_seniors = 0;
                _m_buildings.m_buffer[(int)building].m_fireHazard = 0;
                _m_buildings.m_buffer[(int)building].m_electricityProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_waterProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_workerProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_incomingProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_outgoingProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_healthProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_deathProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_serviceProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_taxProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_majorProblemTimer = 0;
                _m_buildings.m_buffer[(int)building].m_levelUpProgress = 0;
                _m_buildings.m_buffer[(int)building].m_subCulture = 0;
                info.m_buildingAI.CreateBuilding(building, ref _m_buildings.m_buffer[(int)building]);
                _m_buildings.m_buffer[(int)building].m_frame1 = _m_buildings.m_buffer[(int)building].m_frame0;
                _m_buildings.m_buffer[(int)building].m_frame2 = _m_buildings.m_buffer[(int)building].m_frame0;
                _m_buildings.m_buffer[(int)building].m_frame3 = _m_buildings.m_buffer[(int)building].m_frame0;
                InitializeBuilding(building, ref _m_buildings.m_buffer[(int)building]); // Avoid reflection

                var _this = Singleton<BuildingManager>.instance;
                _this.UpdateBuilding(building);
                _this.UpdateBuildingColors(building);
                _this.m_buildingCount = (int)(_m_buildings.ItemCount() - 1u);

                return true;
            }
            building = 0;
            return false;
        }

        private void InitializeBuilding(ushort building, ref Building data)
        {
            var _this = Singleton<BuildingManager>.instance;
            
            int num = Mathf.Clamp((int)(data.m_position.x / 64f + 135f), 0, 269);
            int num2 = Mathf.Clamp((int)(data.m_position.z / 64f + 135f), 0, 269);
            int num3 = num2 * 270 + num;
            while (!Monitor.TryEnter(_this.m_buildingGrid, SimulationManager.SYNCHRONIZE_TIMEOUT))
            {
            }
            try
            {
                _this.m_buildings.m_buffer[(int)building].m_nextGridBuilding = _this.m_buildingGrid[num3];
                _this.m_buildingGrid[num3] = building;
            }
            finally
            {
                Monitor.Exit(Singleton<BuildingManager>.instance.m_buildingGrid);
            }
        }
    }
}
