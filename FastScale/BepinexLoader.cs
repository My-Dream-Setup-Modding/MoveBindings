using BepInEx;
using BepInEx.Logging;
using FastScale.Patches;
using HarmonyLib;
using Placing;
using Sirenix.Utilities;
using System;
using System.Linq;
using UnityEngine;

namespace FastScale
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepinexLoader : BaseUnityPlugin
    {
        public const string
        MODNAME = "ScaleExpander",
        AUTHOR = "Endskill",
        GUID = AUTHOR + "." + MODNAME,
        VERSION = "1.0.0";

        public static Harmony ScaleExpanderHarmony { get; } = new Harmony(MODNAME);
        public static ManualLogSource Log { get; set; }
        public static GameObject CurrentSelectedObject { get; set; }

        private Transform _currentObject;

        public void Awake()
        {
            Log = Logger;
            ScaleExpanderHarmony.PatchAll(typeof(ObjectSelectingPatches));
            Logger.LogWarning("UwU");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.RightControl))
            {
                //Change current selected object.
                _currentObject = CurrentSelectedObject.transform;
                LogManager.Message($"New object selected {_currentObject.name}");
            }
        }

        public void FixedUpdate()
        {
            if (_currentObject is null)
                return;

            if (BothPressed(new[] { KeyCode.RightShift, KeyCode.Keypad0 }, new[] { KeyCode.Keypad5, KeyCode.Y })
                || CheckPressed(new[] { KeyCode.Keypad5, KeyCode.Y }))
            {
                //move north
                var vec = _currentObject.transform.localPosition;
                LogManager.Message($"Keypad 5 while _object not null, before change: {vec.x} {vec.y} {vec.z}");
                _currentObject.localPosition = new Vector3(vec.x + 0.005f, vec.y, vec.z);
                vec = _currentObject.localPosition;
                LogManager.Message($"Keypad 5 while _object not null, before change: {vec.x} {vec.y} {vec.z}");
            }

            if (BothPressed(new[] { KeyCode.RightShift, KeyCode.Keypad0 }, new[] { KeyCode.Keypad3, KeyCode.G })
                || CheckPressed(new[] { KeyCode.Keypad3, KeyCode.G }))
            {
                //move east
                var vec = _currentObject.transform.localPosition;
                _currentObject.localPosition = new Vector3(vec.x, vec.y, vec.z + 0.005f);
            }

            if (BothPressed(new[] { KeyCode.RightShift, KeyCode.Keypad0 }, new[] { KeyCode.Keypad2, KeyCode.H })
                || CheckPressed(new[] { KeyCode.Keypad2, KeyCode.H }))
            {
                //move south
                var vec = _currentObject.transform.localPosition;
                _currentObject.localPosition = new Vector3(vec.x - 0.005f, vec.y, vec.z);
            }

            if (BothPressed(new[] { KeyCode.RightShift, KeyCode.Keypad0 }, new[] { KeyCode.Keypad1, KeyCode.J })
                || CheckPressed(new[] { KeyCode.Keypad1, KeyCode.J }))
            {
                //move west
                var vec = _currentObject.transform.localPosition;
                _currentObject.localPosition = new Vector3(vec.x, vec.y, vec.z - 0.005f);
            }

            if (BothPressed(new[] { KeyCode.RightShift, KeyCode.Keypad0 }, new[] { KeyCode.Keypad9, KeyCode.I })
                || CheckPressed(new[] { KeyCode.Keypad9, KeyCode.I }))
            {
                //move up
                var vec = _currentObject.transform.localPosition;
                _currentObject.localPosition = new Vector3(vec.x, vec.y + 0.005f, vec.z);
            }

            if (BothPressed(new[] { KeyCode.RightShift, KeyCode.Keypad0 }, new[] { KeyCode.Keypad6, KeyCode.K })
                || CheckPressed(new[] { KeyCode.Keypad6, KeyCode.K }))
            {
                //move down
                var vec = _currentObject.transform.localPosition;
                _currentObject.localPosition = new Vector3(vec.x, vec.y - 0.005f, vec.z);
            }

            //Scaling
            if (BothPressed(new[] { KeyCode.RightShift, KeyCode.Keypad0 }, new[] { KeyCode.Keypad7, KeyCode.O })
                || CheckPressed(new[] { KeyCode.Keypad7, KeyCode.O }))
            {
                //scale up
                var vec = _currentObject.transform.localScale;
                _currentObject.localScale = new Vector3(vec.x + 0.3f, vec.y + 0.3f, vec.z + 0.3f);
            }

            if (BothPressed(new[] { KeyCode.RightShift, KeyCode.Keypad0 }, new[] { KeyCode.Keypad4, KeyCode.L })
                || CheckPressed(new[] {KeyCode.Keypad4, KeyCode.L}))
            {
                //scale down
                var vec = _currentObject.transform.localScale;
                _currentObject.localScale = new Vector3(vec.x - 0.3f, vec.y- 0.3f, vec.z - 0.3f);
            }

            //rotate

            //if(Input.GetKeyDown(KeyCode.UpArrow))
            //{
            //    var vec = _currentObject.transform.localRotation;
            //    _currentObject.localRotation = new Vector3(vec.x  0.3f, vec.y - 0.3f, vec.z - 0.3f);
            //}
        }

        private bool BothPressed(KeyCode[] firstButton, KeyCode[] second)
        {
            return firstButton.Select(x => Input.GetKey(x)).Any(x => x) && second.Select(x => Input.GetKey(x)).Any(x => x);
        }

        private bool CheckPressed(KeyCode[] button)
        {
            //for(int i = 0; i < 2; i++)
            //{
            //    if (Input.GetKeyDown(button[i]))
            //        return true;
            //}

            //return false;

            return button.Select(x => Input.GetKeyDown(x)).Any(x => x);
        }
    }
}
