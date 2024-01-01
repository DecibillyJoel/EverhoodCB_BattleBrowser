using BepInEx.Logging;
using EverhoodModding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EverhoodCB_BattleBrowser
{
    public class BrowserPatches
    {
        public static int modIndex = 0;

        public static bool CreateBattleSlotUI_BBPatch(
            List<EverhoodModInstaller.ModInfos> ___modInfos
            ,List<BattleSlotUI> ___battleSlotUIs
        )
        {
            
            for (int slotNum = 0; slotNum < ___battleSlotUIs.Count; slotNum++)
            {
                ___battleSlotUIs[slotNum].gameObject.SetActive(false);
            }
            int offset = modIndex;
            for (int modInfoNum = offset; modInfoNum < ___modInfos.Count && (modInfoNum - offset) < ___battleSlotUIs.Count; modInfoNum++)
            {
                ___battleSlotUIs[modInfoNum - offset].gameObject.SetActive(true);
                ___battleSlotUIs[modInfoNum - offset].UpdateUI(___modInfos[modInfoNum]);
            }

            return false;
        }

        public static void UpdateBattleSlotUI(EverhoodModInstaller everhoodMI)
        {
            BindingFlags flagsPrivInst = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo createBattleSlotUI = everhoodMI.GetType().GetMethod("CreateBattleSlotUI", flagsPrivInst);
            createBattleSlotUI.Invoke(everhoodMI, new object[] { });
        }
    }
}
