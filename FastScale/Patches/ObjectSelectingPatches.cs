using HarmonyLib;
using Placing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace FastScale.Patches
{
    [HarmonyPatch(typeof(ObjectSelecting))]
    public static class ObjectSelectingPatches
    {
        [HarmonyPatch(nameof(ObjectSelecting.SelectObject))]
        [HarmonyPostfix]
        public static void SelectObjectPostfix(ObjectSelecting __instance)
        {
            BepinexLoader.CurrentSelectedObject = __instance._selectedObject;
            LogManager.Message($"New object selected {BepinexLoader.CurrentSelectedObject?.name ?? "null"}");
        }

    }
}
