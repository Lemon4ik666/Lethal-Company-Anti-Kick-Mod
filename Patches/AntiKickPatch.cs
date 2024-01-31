using GameNetcodeStuff;
using HarmonyLib;
using Lethal_Company_Mod;
using Steamworks;

[HarmonyPatch(typeof(PlayerControllerB), "SendNewPlayerValuesServerRpc")]
class AntiKickPatch
{
    static bool Prefix(PlayerControllerB __instance)
    {
        if (!ModConfig.EnableAntiKick.Value) return true; // Включение отключение мода

        ulong[] playerSteamIds = new ulong[__instance.playersManager.allPlayerScripts.Length];

        for (int i = 0; i < __instance.playersManager.allPlayerScripts.Length; i++)
        {
            playerSteamIds[i] = __instance.playersManager.allPlayerScripts[i].playerSteamId;
        }

        playerSteamIds[__instance.playerClientId] = SteamClient.SteamId;

        _ = __instance.Reflect().InvokeInternalMethod("SendNewPlayerValuesClientRpc", playerSteamIds);

        return false;
    }
}
