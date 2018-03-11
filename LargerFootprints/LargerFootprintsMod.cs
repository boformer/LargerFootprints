using ICities;
using System.Reflection;

namespace LargerFootprints
{
    public class LargerFootprintsMod : LoadingExtensionBase, IUserMod
    {
        // This is limited by the usage of "byte" in the Building struct
        // The maximum would be 255
        // There is a bug that messes up the ground texture placement 
        // in the asset editor. It occurs when assets are larger than 32x32.
        // 32 blocks are 256m
        public const int MAX_LENGTH = 32;
        public const int MAX_WIDTH = 32;

        public string Name
        {
            get
            {
                return "Larger Footprints";
            }
        }

        public string Description
        {
            get
            {
                return "Create & use larger ploppable assets";
            }
        }

        public override void OnCreated(ILoading loading)
        {
            // Modify setters Building.Width/Building.Length:
            // Those are properties of the Building struct
            // The setters clamp the width and length to 16
            Detour.BuildingDetour.Deploy();

            // Replace getWidthRange/getLengthRange methods of all ploppable BuildingAIs
            // By default these are limiting the asset size to 
            // 16x8, 15x9, 14x11, 13x12, 12x13, 11x14, 9x15, 8x16
            // The modified methods return 32x32
            Detour.BuildingAIDetour<CargoStationAI>.Deploy();
            Detour.BuildingAIDetour<CargoHarborAI>.Deploy();
            Detour.BuildingAIDetour<CemeteryAI>.Deploy();
            Detour.BuildingAIDetour<DepotAI>.Deploy();
            Detour.BuildingAIDetour<TransportStationAI>.Deploy();
            Detour.BuildingAIDetour<HarborAI>.Deploy();
            Detour.BuildingAIDetour<FireStationAI>.Deploy();
            Detour.BuildingAIDetour<HospitalAI>.Deploy();
            Detour.BuildingAIDetour<MedicalCenterAI>.Deploy();
            Detour.BuildingAIDetour<LandfillSiteAI>.Deploy();
            Detour.BuildingAIDetour<MonumentAI>.Deploy();
            Detour.BuildingAIDetour<ParkAI>.Deploy();
            Detour.BuildingAIDetour<PoliceStationAI>.Deploy();
            Detour.BuildingAIDetour<PowerPlantAI>.Deploy();
            Detour.BuildingAIDetour<WindTurbineAI>.Deploy();
            Detour.BuildingAIDetour<SchoolAI>.Deploy();
            Detour.BuildingAIDetour<WaterFacilityAI>.Deploy();
            Detour.BuildingAIDetour<HadronColliderAI>.Deploy();
            //Detour.BuildingAIDetour<EdenProjectAI>.Deploy();
            //Detour.BuildingAIDetour<FusionPowerPlantAI>.Deploy();
            //Detour.BuildingAIDetour<DamPowerHouseAI>.Deploy();
            //Detour.BuildingAIDetour<SpaceElevatorAI>.Deploy();

            // This is a buffer that has a length of 16 by default
            // It is used by the game to calculate the ground texture of an asset
            typeof(Building).GetField("TempMask", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, new ushort[MAX_LENGTH]);
        }
        public override void OnReleased()
        {
            Detour.BuildingDetour.Revert();

            Detour.BuildingAIDetour<CargoStationAI>.Revert();
            Detour.BuildingAIDetour<CargoHarborAI>.Revert();
            Detour.BuildingAIDetour<CemeteryAI>.Revert();
            Detour.BuildingAIDetour<DepotAI>.Revert();
            Detour.BuildingAIDetour<TransportStationAI>.Revert();
            Detour.BuildingAIDetour<HarborAI>.Revert();
            Detour.BuildingAIDetour<FireStationAI>.Revert();
            Detour.BuildingAIDetour<HospitalAI>.Revert();
            Detour.BuildingAIDetour<MedicalCenterAI>.Revert();
            Detour.BuildingAIDetour<LandfillSiteAI>.Revert();
            Detour.BuildingAIDetour<MonumentAI>.Revert();
            Detour.BuildingAIDetour<ParkAI>.Revert();
            Detour.BuildingAIDetour<PoliceStationAI>.Revert();
            Detour.BuildingAIDetour<PowerPlantAI>.Revert();
            Detour.BuildingAIDetour<WindTurbineAI>.Revert();
            Detour.BuildingAIDetour<SchoolAI>.Revert();
            Detour.BuildingAIDetour<WaterFacilityAI>.Revert();
            Detour.BuildingAIDetour<HadronColliderAI>.Revert();
            //Detour.BuildingAIDetour<EdenProjectAI>.Revert();
            //Detour.BuildingAIDetour<FusionPowerPlantAI>.Revert();
            //Detour.BuildingAIDetour<DamPowerHouseAI>.Revert();
            //Detour.BuildingAIDetour<SpaceElevatorAI>.Revert();
        }
    }
}
