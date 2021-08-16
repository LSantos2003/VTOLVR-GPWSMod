using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GPWS
{

    [HarmonyPatch(typeof(Actor), "Start")]
    class PlayerSpawnPatch
    {
        public static void Postfix(Actor __instance)
        {
            if (__instance.isPlayer)
            {
                FlightLogger.Log("Adding gpws!");
                __instance.gameObject.AddComponent<GPWS>();
            }
        }
    }

}

