using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;

using Artwork = RenderFixMaybe.Resources.costs;

namespace renderFixMaybe
{
	[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
	[BepInDependency(APIGUID, BepInDependency.DependencyFlags.HardDependency)]
	public partial class Plugin : BaseUnityPlugin
	{
		public const string APIGUID = "cyantist.inscryption.api";
		public const string PluginGuid = "extraVoid.inscryption.renderPatcher";
		private const string PluginName = "Cost Render Fix Port";
		private const string PluginVersion = "1.1.0";

		public static string Directory;
		internal static ManualLogSource Log;

		public static float Yposition = 0.85f;
		public static float Xposition = 0.4f;
		public static float pixelPerUnity = 100.0f;
		public static Vector2 vector = new Vector2(Xposition, Yposition);


		private void Awake()
		{
			Log = base.Logger;

			Harmony harmony = new(PluginGuid);
			harmony.PatchAll();
		}


		public static Sprite LoadSpriteFromResource(byte[] resourceFile)
		{
			var texture = new Texture2D(2, 2);
			texture.LoadImage(resourceFile);
			texture.filterMode = FilterMode.Point;
			var sprite = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), vector, pixelPerUnity);
			return sprite;
		}

		public static Sprite MakeSpriteFromTexture2D(Texture2D texture)
		{
			var sprite = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), vector, pixelPerUnity);
			return sprite;
		}

		public static Texture2D LoadTextureFromResource(byte[] resourceFile)
		{
			var texture = new Texture2D(2, 2);
			texture.LoadImage(resourceFile);
			texture.filterMode = FilterMode.Point;
			return texture;
		}



		private static Texture2D CombineTextures(List<Texture2D> abilities, List<Vector2Int> patchlocations)
		{
			bool flag = abilities != null;
			Texture2D result;
			if (flag)
			{
				Texture2D texture2D2 = LoadTextureFromResource(Artwork.empty_cost);
				for (int j = 0; j < abilities.Count; j++)
				{
					int index = j;
					texture2D2.SetPixels(patchlocations[index].x, patchlocations[index].y, abilities[index].width, abilities[index].height, abilities[index].GetPixels(), 0);
				}
				texture2D2.Apply();
				result = texture2D2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		private static Texture2D CombineMoxTextures(List<Texture2D> abilities, List<Vector2Int> patchlocations)
		{
			bool flag = abilities != null;
			Texture2D result;
			if (flag)
			{
				Texture2D texture2D2 = LoadTextureFromResource(Artwork.mox_cost_empty);
				for (int j = 0; j < abilities.Count; j++)
				{
					int index = j;
					texture2D2.SetPixels(patchlocations[index].x, patchlocations[index].y, abilities[index].width, abilities[index].height, abilities[index].GetPixels(), 0);
				}
				texture2D2.Apply();
				result = texture2D2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static List<Vector2Int> fourCost = new List<Vector2Int>
		{
			new Vector2Int(0, 0),
			new Vector2Int(0, 28),
			new Vector2Int(0, 56),
			new Vector2Int(0, 84)
		};

		public static List<Vector2Int> threeCost = new List<Vector2Int>
		{
			new Vector2Int(0, 28),
			new Vector2Int(0, 56),
			new Vector2Int(0, 84)
		};

		public static List<Vector2Int> twoCost = new List<Vector2Int>
		{
			new Vector2Int(0, 56),
			new Vector2Int(0, 84)
		};

		public static List<Vector2Int> oneCost = new List<Vector2Int>
		{
			new Vector2Int(0, 84)
		};

		public static List<Vector2Int> moxCost = new List<Vector2Int>
		{
			new Vector2Int(0, 0),
			new Vector2Int(21, 0),
			new Vector2Int(42, 0)
		};


		[HarmonyPatch(typeof(CardDisplayer), nameof(CardDisplayer.GetCostSpriteForCard))]
		public class CostDisplayFixMaybe
		{
			[HarmonyPostfix]
			public static void Postfix(ref Sprite __result, ref CardInfo card)
			{

				//Make sure we are in Leshy's Cabin
				bool flag = SceneLoader.ActiveSceneName == "Part1_Cabin";
				if (flag) 
				{ 
					//Make the texture variables and set them to the default (which is 0)
					Texture2D texBloodCost = LoadTextureFromResource(Artwork.blood_cost_0);
					Texture2D texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_0);
					Texture2D texEnergyCost = LoadTextureFromResource(Artwork.energy_cost_0);
					Texture2D textGemCost = LoadTextureFromResource(Artwork.mox_cost_empty);
	
					//A list to hold the textures (important later, to combine them all)
					List<Texture2D> list = new List<Texture2D>();
	
					//Get the costs of blood, bone, and energy
					int bloodCost = card.BloodCost;
					int boneCost = card.BonesCost;
					int energyCost = card.energyCost;
	
					//Switch statement to set the blood texture to the right cost
					switch (bloodCost)
					{
						case 1:
							texBloodCost = LoadTextureFromResource(Artwork.blood_cost_1);
							break;
						case 2:
							texBloodCost = LoadTextureFromResource(Artwork.blood_cost_2);
							break;
						case 3:
							texBloodCost = LoadTextureFromResource(Artwork.blood_cost_3);
							break;
						case 4:
							texBloodCost = LoadTextureFromResource(Artwork.blood_cost_4);
							break;
					}
	
					//Switch statement to set the energy texture to the right cost
					switch (energyCost)
					{
						case 1:
							texEnergyCost = LoadTextureFromResource(Artwork.energy_cost_1);
							break;
						case 2:
							texEnergyCost = LoadTextureFromResource(Artwork.energy_cost_2);
							break;
						case 3:
							texEnergyCost = LoadTextureFromResource(Artwork.energy_cost_3);
							break;
						case 4:
							texEnergyCost = LoadTextureFromResource(Artwork.energy_cost_4);
							break;
						case 5:
							texEnergyCost = LoadTextureFromResource(Artwork.energy_cost_5);
							break;
						case 6:
							texEnergyCost = LoadTextureFromResource(Artwork.energy_cost_6);
							break;
					}
	
					//Switch statement to set the bone texture to the right cost
					switch (boneCost)
					{
						case 1:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_1);
							break;
						case 2:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_2);
							break;
						case 3:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_3);
							break;
						case 4:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_4);
							break;
						case 5:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_5);
							break;
						case 6:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_6);
							break;
						case 7:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_7);
							break;
						case 8:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_8);
							break;
						case 9:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_9);
							break;
						case 10:
							texBoneCcost = LoadTextureFromResource(Artwork.bone_cost_10);
							break;
					}
	
					//make a new list for the mox textures
					List<Texture2D> gemCost = new List<Texture2D>();
					//load up the mox textures as "empty"
					Texture2D orange = LoadTextureFromResource(Artwork.mox_cost_e);
					Texture2D blue = LoadTextureFromResource(Artwork.mox_cost_e);
					Texture2D green = LoadTextureFromResource(Artwork.mox_cost_e);
	
					//If a card has a green mox, set the green mox
					if (card.GemsCost.Contains(GemType.Green))
					{
						green = LoadTextureFromResource(Artwork.mox_cost_g);
						Log.LogWarning("found green mox in: " + card.name);
					}
					//If a card has a green mox, set the Orange mox
					if (card.GemsCost.Contains(GemType.Orange))
					{
						orange = LoadTextureFromResource(Artwork.mox_cost_o);
						Log.LogWarning("found orange mox in: " + card.name);
					}
					//If a card has a green mox, set the Blue mox
					if (card.GemsCost.Contains(GemType.Blue))
					{
						blue = LoadTextureFromResource(Artwork.mox_cost_b);
						Log.LogWarning("found blue mox in: " + card.name);
					}
					//Add all moxes to the gemcost list
					gemCost.Add(orange);
					gemCost.Add(green);
					gemCost.Add(blue);
					//Combine the textures into one
					Texture2D finalMoxTexture = CombineMoxTextures(gemCost, moxCost);
	
					//Add the textures if greater than 0 to the list
					//we do it like this, so in case if it has just a bone cost, it will appear at the start instead of in the middle of the card
					if (card.GemsCost.Count > 0)
					{
						list.Add(finalMoxTexture);
					}
					if (energyCost > 0)
					{
						list.Add(texEnergyCost);
					}
					if (boneCost > 0)
					{
						list.Add(texBoneCcost);
					}
					if (bloodCost > 0)
					{
						list.Add(texBloodCost);
					}
	
					//Make sure to use the right vector for the amount of items.
					//So count the list and use a switch statement to pick the right one.
					//If it is 0, just add them all to the list.
					var counting = list.Count;
					var total = new List<Vector2Int>();
					switch (counting)
					{
						case 0:
							list.Add(textGemCost);
							list.Add(texEnergyCost);
							list.Add(texBoneCcost);
							list.Add(texBloodCost);
							total = fourCost;
							break;
						case 1:
							total = oneCost;
							break;
						case 2:
							total = twoCost;
							break;
						case 3:
							total = threeCost;
							break;
						case 4:
							total = fourCost;
							break;
					}
	
					//Combine all the textures from the list into one texture
					Texture2D finalTexture = CombineTextures(list, total);
	
					//Convert the final texture to a sprite
					Sprite finalSprite = MakeSpriteFromTexture2D(finalTexture);
	
					/// Set the results as the new sprite
					__result = finalSprite;
				}
			}
		}
	}
}
