using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace $safeprojectname$
{
	[BepInPlugin("your.unique.mod.identifier", Plugin.ModName, Plugin.Version)]
	public class Plugin : BaseUnityPlugin
	{
		public const string Version = "1.0";
		public const string ModName = "My Mod";
		Harmony _Harmony;
		public static ManualLogSource Log;
		public static GameObject PrefabOwner;

		private void Awake()
		{
#if DEBUG
			Log = Logger;
#else
			Log = new ManualLogSource(null);
#endif

			if (!PrefabOwner)
			{
				PrefabOwner = new GameObject(ModName + "PrefabOwner");
				PrefabOwner.SetActive(false);
				DontDestroyOnLoad(PrefabOwner);
			}

			_Harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
		}

		private void OnDestroy()
		{
			if (_Harmony != null) _Harmony.UnpatchSelf();
			AssetHelper.AssetCleanup();
		}
	}
}
