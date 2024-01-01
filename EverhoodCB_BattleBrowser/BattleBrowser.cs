using BepInEx;
using BepInEx.Logging;
using EverhoodModding;
using HarmonyLib;
using HarmonyLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EverhoodCB_BattleBrowser
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class BattleBrowser : BaseUnityPlugin
    {
        public const string pluginGuid = "v8ninja.everhoodcb.battlebrowser";
        public const string pluginName = "Battle Browser (by V8_Ninja)";
        public const string pluginVersion = "0.0.1";

        private EverhoodModInstaller everhoodMI;

        public void Start()
        {
            // Getting the Everhood Mod Installer instance
            everhoodMI = FindObjectOfType<EverhoodModInstaller>();
            if (everhoodMI == null)
            {
                Logger.LogError("Could not find EverhoodModInstaller instance!");
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
            if (everhoodMI == null)
            {
                everhoodMI = FindObjectOfType<EverhoodModInstaller>();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                BrowserPatches.modIndex++;
                BrowserPatches.UpdateBattleSlotUI(everhoodMI);
            }
            if (Input.GetKeyDown(KeyCode.Q) && BrowserPatches.modIndex > 0)
            {
                BrowserPatches.modIndex--;
                BrowserPatches.UpdateBattleSlotUI(everhoodMI);
            }
        }
    }
}
