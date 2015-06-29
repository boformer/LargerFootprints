using System.Reflection;
using UnityEngine;

namespace LargerFootprints.Detour
{
    public class BuildingAIDetour<A> where A : PlayerBuildingAI
    {
        private static bool deployed = false;

        private static RedirectCallsState _AI_GetWidthRange_state;
        private static MethodInfo _AI_GetWidthRange_original;
        private static MethodInfo _AI_GetWidthRange_detour;

        private static RedirectCallsState _AI_GetLengthRange_state;
        private static MethodInfo _AI_GetLengthRange_original;
        private static MethodInfo _AI_GetLengthRange_detour;

        public static void Deploy()
        {
            if (!deployed)
            {
                _AI_GetWidthRange_original = typeof(A).GetMethod("GetWidthRange", BindingFlags.Instance | BindingFlags.Public);
                _AI_GetWidthRange_detour = typeof(BuildingAIDetour<A>).GetMethod("GetWidthRange", BindingFlags.Instance | BindingFlags.Public);
                _AI_GetWidthRange_state = RedirectionHelper.RedirectCalls(_AI_GetWidthRange_original, _AI_GetWidthRange_detour);

                _AI_GetLengthRange_original = typeof(A).GetMethod("GetLengthRange", BindingFlags.Instance | BindingFlags.Public);
                _AI_GetLengthRange_detour = typeof(BuildingAIDetour<A>).GetMethod("GetLengthRange", BindingFlags.Instance | BindingFlags.Public);
                _AI_GetLengthRange_state = RedirectionHelper.RedirectCalls(_AI_GetLengthRange_original, _AI_GetLengthRange_detour);

                deployed = true;

                Debug.LogFormat("LargerFootprints: {0} Methods detoured!", typeof(A).Name);
            }
        }

        public static void Revert()
        {
            if (deployed)
            {
                RedirectionHelper.RevertRedirect(_AI_GetWidthRange_original, _AI_GetWidthRange_state);
                _AI_GetWidthRange_original = null;
                _AI_GetWidthRange_detour = null;

                RedirectionHelper.RevertRedirect(_AI_GetLengthRange_original, _AI_GetLengthRange_state);
                _AI_GetLengthRange_original = null;
                _AI_GetLengthRange_detour = null;

                deployed = false;

                Debug.LogFormat("LargerFootprints: {0} Methods restored!", typeof(A).Name);
            }
        }

        public virtual void GetWidthRange(out int minWidth, out int maxWidth)
        {
            //Debug.Log("GetWidthRange called.");

            minWidth = 1;
            maxWidth = LargerFootprintsMod.MAX_WIDTH;
        }

        public virtual void GetLengthRange(out int minLength, out int maxLength)
        {
            //Debug.Log("GetLengthRange called.");

            minLength = 1;
            maxLength = LargerFootprintsMod.MAX_LENGTH;
        }
    }
}
