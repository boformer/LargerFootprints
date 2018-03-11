using System.Reflection;

namespace LargerFootprints.Detour
{
    public struct BuildingDetour
    {
                private static bool deployed = false;

        private static RedirectCallsState _Building_SetWidth_state;
        private static RedirectCallsState _Building_SetLength_state;

        private static MethodInfo _Building_SetWidth_original;
        private static MethodInfo _Building_SetLength_original;

        private static MethodInfo _Building_SetWidth_detour;
        private static MethodInfo _Building_SetLength_detour;


        public static void Deploy()
        {
            if (!deployed)
            {
                _Building_SetWidth_original = typeof(Building).GetProperty("Width").GetSetMethod();
                _Building_SetWidth_detour = typeof(BuildingDetour).GetMethod("set_Width", BindingFlags.Static | BindingFlags.Public);
                _Building_SetWidth_state = RedirectionHelper.RedirectCalls(_Building_SetWidth_original, _Building_SetWidth_detour);

                _Building_SetLength_original = typeof(Building).GetProperty("Length").GetSetMethod();
                _Building_SetLength_detour = typeof(BuildingDetour).GetMethod("set_Length", BindingFlags.Static | BindingFlags.Public);
                _Building_SetLength_state = RedirectionHelper.RedirectCalls(_Building_SetLength_original, _Building_SetLength_detour);

                deployed = true;

                UnityEngine.Debug.Log("LargerFootprints: Building Methods detoured!");
            }
        }

        public static void Revert()
        {
            if (deployed)
            {
                RedirectionHelper.RevertRedirect(_Building_SetWidth_original, _Building_SetWidth_state);
                _Building_SetWidth_original = null;
                _Building_SetWidth_detour = null;

                RedirectionHelper.RevertRedirect(_Building_SetLength_original, _Building_SetLength_state);
                _Building_SetLength_original = null;
                _Building_SetLength_detour = null;

                deployed = false;

                UnityEngine.Debug.Log("LargerFootprints: Building Methods restored!");
            }
        }


        public static void set_Width(ref Building building, int value)
        {
            //UnityEngine.Debug.Log("Set width detour: " + value);
            building.m_width = (byte) value;
        }

        public static void set_Length(ref Building building, int value)
        {
            //UnityEngine.Debug.Log("Set length detour: " + value);
            building.m_length = (byte) value;
        }
    }
}