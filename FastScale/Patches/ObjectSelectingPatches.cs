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
            var test = typeof(ObjectSelecting).GetField(nameof(ObjectSelecting._selectedObject), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            BepinexLoader.CurrentSelectedObject = (GameObject) test;
            LogManager.Message($"New object selected {BepinexLoader.CurrentSelectedObject?.name ?? "null"}");
        }

    }
}
