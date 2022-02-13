using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using UnityEngine;
using HarmonyLib;
using GBC;

using BepInEx.Configuration;

namespace RenderFixMaybe
{
	[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
	[BepInDependency(APIGUID, BepInDependency.DependencyFlags.HardDependency)]
	public partial class Plugin : BaseUnityPlugin
	{
		public const string APIGUID = "cyantist.inscryption.api";
		public const string PluginGuid = "extraVoid.inscryption.renderPatcher";
		private const string PluginName = "Cost Render Fix";
		private const string PluginVersion = "1.3.0";

		public static string Directory;
		internal static ManualLogSource Log;


		internal static ConfigEntry<bool> configRightCorner;
		internal static ConfigEntry<bool> configAct2;


		private void Awake()
		{
			Log = base.Logger;


			configRightCorner = Config.Bind("Act 2 Cost corner", "Costs in the top right corner", true, "If this is true, cost will be in the top right corner. If it is false, it will be in top left corner");
			configAct2 = Config.Bind("Render Fix", "Render Fix", true, "Turn on / off the render fix for costs. This will cause hybrid costs to not show up in act 1 without decals, and will cause them to not show up at all in act 2. Also cards with more than vanilla costs will break (I.E. a 5 blood card, a 14 bone card)");

			if (configAct2.Value == true) {
				Harmony harmony = new(PluginGuid);
				harmony.PatchAll();
			}
		}


		[HarmonyPatch(typeof(CardDisplayer), nameof(CardDisplayer.GetCostSpriteForCard))]
		public class CostDisplayFixMaybe
		{
			[HarmonyPrefix]
			public static bool Prefix(ref Sprite __result, ref CardInfo card)
			{

				string flag = SceneLoader.ActiveSceneName;

				switch (flag)
                {
					///Leshy's Cabin main gameplay area
					case "Part1_Cabin":
						__result = RenderFixMaybe.Part1CostRender.Part1SpriteFinal(card);
						return false;
					///Leshy when making a death card
					case "Part1_Sanctum":
						__result = RenderFixMaybe.Part1CostRender.Part1SpriteFinal(card);
						return false;
					///Grimora in the final
					case "finale_grimora":
						return true;
					///Magnificus in the final
					case "finale_magnificus":
						return true;
					///I dont know what this scene is but I dont want to edit cards here
					case "finale_redacted":
						return true;
					///All of P03's factory
					case "Part3_Cabin":
						return true;
					///If it's none of the above areas, then it's somewhere in act 2
					default:
						if (configRightCorner.Value == true)
						{
							__result = RenderFixMaybe.Part2CostRender_Right.Part2SpriteFinal(card);
						}
						else
						{
							__result = RenderFixMaybe.Part2CostRender_Left.Part2SpriteFinal(card);
						}
						return false;
				}
			}
        }
	}
}
