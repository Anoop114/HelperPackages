using System.Collections.Generic;
using UnityEngine;

namespace HelperFunction.Routines
{
    public static class RoutinesHelpers
    {
        private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsMap = new();

        public static WaitForSeconds GetWait(float time)
        {
            if (WaitForSecondsMap.TryGetValue(time, out var wait)) return wait;

            WaitForSecondsMap[time] = new WaitForSeconds(time);
            return WaitForSecondsMap[time];
        }
    }
}