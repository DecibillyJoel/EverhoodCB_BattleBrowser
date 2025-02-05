using BepInEx;
using BepInEx.Logging;
using EverhoodModding;
using HarmonyLib;
using HarmonyLib.Tools;
using Rewired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace EverhoodCB_BattleBrowser
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class BattleBrowser : BaseUnityPlugin
    {
        public const string pluginGuid = "v8ninja.everhoodcb.battlebrowser";
        public const string pluginName = "Battle Browser (by V8_Ninja)";
        public const string pluginVersion = "0.0.2";

        public static KeyCode prevKey;
        public static KeyCode nextKey;
        public static int nextButton;
        public static int prevButton;
        public static string sceneName;

        private EverhoodModInstaller everhoodMI;
        private Joystick input = null;

        public void Awake()
        {
            // Setting default options
            prevKey = KeyCode.Q;
            nextKey = KeyCode.E;
            nextButton = 5;
            prevButton = 4;
            sceneName = SceneManager.GetActiveScene().name;
        }

        public void Start()
        {
            // Getting the Everhood Mod Installer instance
            everhoodMI = FindObjectOfType<EverhoodModInstaller>();
            if (everhoodMI == null)
            {
                Logger.LogError("Could not find EverhoodModInstaller instance!");
            }

            // Getting initial joystick (if it exists)
            if (EverhoodInput.player.controllers.joystickCount > 0)
            {
                input = EverhoodInput.player.controllers.Joysticks[0];
            }

            // Patching DLL methods
            Harmony hrm = new Harmony(pluginGuid);

            MethodInfo orgCBSUI = AccessTools.Method(typeof(EverhoodModInstaller), "CreateBattleSlotUI");
            MethodInfo newCBSUI = AccessTools.Method(typeof(BrowserPatches), "CreateBattleSlotUI_BBPatch");
            hrm.Patch(orgCBSUI, new HarmonyMethod(newCBSUI));
            

            // Posting A-Okay message in logging
            if (everhoodMI != null)
            {
                Logger.LogInfo("Mod done loading!");
            }
        }

        public void Update()
        {
            if (SceneManager.GetActiveScene().name != sceneName) return;

            if (input == null && EverhoodInput.player.controllers.joystickCount > 0)
            {
                input = EverhoodInput.player.controllers.Joysticks[0];
            }

            if (everhoodMI == null)
            {
                everhoodMI = FindObjectOfType<EverhoodModInstaller>();
            }
            else
            {
                if (Input.GetKeyDown(nextKey) || (input != null && input.GetButtonDown(nextButton)))
                {
                    BrowserPatches.modIndex++;
                    BrowserPatches.UpdateBattleSlotUI(everhoodMI);
                }
                else if ((Input.GetKeyDown(prevKey) || (input != null && input.GetButtonDown(prevButton))) && BrowserPatches.modIndex > 0)
                {
                    BrowserPatches.modIndex--;
                    BrowserPatches.UpdateBattleSlotUI(everhoodMI);
                }
            }
        }

        public void OnDestroy()
        {
            Logger.LogInfo("Unpatching all methods...");
            Harmony hrm = new Harmony(pluginGuid);
            hrm.UnpatchSelf();
        }
    }
}
