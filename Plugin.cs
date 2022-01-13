using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using UnityEngine;
using HarmonyLib;
using GBC;

using Artwork = RenderFixMaybe.Resources.costs;

namespace RenderFixMaybe
{
	[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
	[BepInDependency(APIGUID, BepInDependency.DependencyFlags.HardDependency)]
	public partial class Plugin : BaseUnityPlugin
	{
		public const string APIGUID = "cyantist.inscryption.api";
		public const string PluginGuid = "extraVoid.inscryption.renderPatcher";
		private const string PluginName = "Cost Render Fix";
		private const string PluginVersion = "1.2.0";

		public static string Directory;
		internal static ManualLogSource Log;

		


		private void Awake()
		{
			Log = base.Logger;

			Harmony harmony = new(PluginGuid);
			harmony.PatchAll();
		}


		[HarmonyPatch(typeof(CardDisplayer), nameof(CardDisplayer.GetCostSpriteForCard))]
		public class CostDisplayFixMaybe
		{
			[HarmonyPrefix]
			public static bool Prefix(ref Sprite __result, ref CardInfo card)
			{
				//Make sure we are in Leshy's Cabin
				bool flag1 = SceneLoader.ActiveSceneName == "Part1_Cabin" || SceneLoader.ActiveSceneName == "Part1_Sanctum";
				if (flag1) 
				{ 
					/// Set the results as the new sprite
					
					__result = RenderFixMaybe.Part1CostRender.Part1SpriteFinal(card);

					return false;
				}


				bool flag2 = SceneLoader.ActiveSceneName.Equals("finale_grimora") || 
					SceneLoader.ActiveSceneName.Equals("finale_magnificus") || 
					SceneLoader.ActiveSceneName.Equals("finale_redacted") ||
					SceneLoader.ActiveSceneName.Equals("Part3_Cabin");
				if (flag2)
                {
					return true;
				}

				__result = RenderFixMaybe.Part2CostRender.Part2SpriteFinal(card);
				return false;
			}
        }

		
	}
}
