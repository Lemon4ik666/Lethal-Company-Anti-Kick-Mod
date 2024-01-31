using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Lethal_Company_Mod;

namespace LethalCompanyMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class MyMod : BaseUnityPlugin
    {
        public const string modGUID = "Lemon4ik.AntiKick"; // Автор
        public const string modName = "Anti Kick"; // Название
        public const string modVersion = "7.7.7"; // Версия

        public readonly Harmony harmony = new Harmony(modGUID);
        public static MyMod Instance; // Объявление переменной Instance
        internal ManualLogSource mls; // Переменная лога

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID); // Ввывод Автора а после сообщения

            mls.LogInfo("Anti_Kick_Loaded"); // Ввывод сообщения о том что Анти Кик загружен!

            // Прочитаем значение конфигурации
            ModConfig.EnableAntiKick = Config.Bind<bool>("General", "AntiKick", true, "Enable or disable (default on)");

            // Патчим только если функция активирована в конфигурации
            if (ModConfig.EnableAntiKick.Value)
            {
                harmony.PatchAll(typeof(AntiKickPatch)); // Загружаем Модуль
            }
        }
    }
}
