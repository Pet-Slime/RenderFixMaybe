using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;

using Art = RenderFixMaybe.Resources.costs;

namespace RenderFixMaybe
{
    public class Part1CostRender
    {
		public static float Yposition = 0.85f;
		public static float Xposition = 0.4f;
		public static float pixelPerUnity = 100.0f;
		public static Vector2 vector = new Vector2(Xposition, Yposition);


		public static Sprite LoadSpriteFromResource(byte[] resourceFile)
		{
			var texture = new Texture2D(2, 2);
			texture.LoadImage(resourceFile);
			texture.filterMode = FilterMode.Point;
			var sprite = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), vector, pixelPerUnity);
			return sprite;
		}

		public static Sprite MakeSpriteFromTexture2D(Texture2D texture, bool flag)
		{
			Sprite sprite;
			float Ytest = 0.85f;
			float Xtest = 0.4f;
			float ppu = 100.0f;
			Vector2 vec = new Vector2(Xtest, Ytest);

			if (flag)
			{
				Ytest = 0.85f;
				Xtest = 0.4f;
				vec = new Vector2(Xtest, Ytest);
				sprite = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), vec, ppu);
			}
			else
			{
				Ytest = 0.0f;
				Xtest = 1.0f;
				vec = new Vector2(Xtest, Ytest);
				sprite = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), vec, ppu);
			}
			return sprite;
		}

		public static Texture2D LoadTextureFromResource(byte[] resourceFile)
		{
			var texture = new Texture2D(2, 2);
			texture.LoadImage(resourceFile);
			texture.filterMode = FilterMode.Point;
			return texture;
		}



		public static Texture2D CombineTextures(List<Texture2D> abilities, List<Vector2Int> patchlocations, byte[] resource)
		{
			bool flag = abilities != null;
			Texture2D result;
			if (flag)
			{
				Texture2D texture2D2 = LoadTextureFromResource(resource);
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

		public static Texture2D CombineMoxTextures(List<Texture2D> abilities, List<Vector2Int> patchlocations)
		{
			bool flag = abilities != null;
			Texture2D result;
			if (flag)
			{
				Texture2D texture2D2 = LoadTextureFromResource(Art.mox_cost_empty);
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


		public static Sprite Part1SpriteFinal(CardInfo card)
		{
			//Make the texture variables and set them to the default (which is 0)
			Texture2D texBloodCost = LoadTextureFromResource(Art.blood_cost_0);
			Texture2D texBoneCcost = LoadTextureFromResource(Art.bone_cost_0);
			Texture2D texEnergyCost = LoadTextureFromResource(Art.energy_cost_0);
			Texture2D textGemCost = LoadTextureFromResource(Art.mox_cost_empty);

			//A list to hold the textures (important later, to combine them all)
			List<Texture2D> list = new List<Texture2D>();

			//Get the costs of blood, bone, and energy
			int bloodCost = card.BloodCost;
			int boneCost = card.BonesCost;
			int energyCost = card.energyCost;
			int moxCost = card.gemsCost.Count;

			//Setting mox first
			if (moxCost > 0)
            {
				//make a new list for the mox textures
				List<Texture2D> gemCost = new List<Texture2D>();
				//load up the mox textures as "empty"
				Texture2D orange = LoadTextureFromResource(Art.mox_cost_e);
				Texture2D blue = LoadTextureFromResource(Art.mox_cost_e);
				Texture2D green = LoadTextureFromResource(Art.mox_cost_e);

				List<Vector2Int> moxVector = new List<Vector2Int>
					{
						new Vector2Int(0, 0),
						new Vector2Int(21, 0),
						new Vector2Int(42, 0)
					};

				//If a card has a green mox, set the green mox
				if (card.GemsCost.Contains(GemType.Green))
				{
					green = LoadTextureFromResource(Art.mox_cost_g);
				}
				//If a card has a green mox, set the Orange mox
				if (card.GemsCost.Contains(GemType.Orange))
				{
					orange = LoadTextureFromResource(Art.mox_cost_o);
				}
				//If a card has a green mox, set the Blue mox
				if (card.GemsCost.Contains(GemType.Blue))
				{
					blue = LoadTextureFromResource(Art.mox_cost_b);
				}
				//Add all moxes to the gemcost list
				gemCost.Add(orange);
				gemCost.Add(green);
				gemCost.Add(blue);
				//Combine the textures into one
				Texture2D finalMoxTexture = CombineMoxTextures(gemCost, moxVector);
				list.Add(finalMoxTexture);
			}

			//Switch Statement to set energy texture to the right cost, and add it to the list if it exists
			switch (energyCost)
			{
				case 1:
					texEnergyCost = LoadTextureFromResource(Art.energy_cost_1);
					list.Add(texEnergyCost);
					break;
				case 2:
					texEnergyCost = LoadTextureFromResource(Art.energy_cost_2);
					list.Add(texEnergyCost);
					break;
				case 3:
					texEnergyCost = LoadTextureFromResource(Art.energy_cost_3);
					list.Add(texEnergyCost);
					break;
				case 4:
					texEnergyCost = LoadTextureFromResource(Art.energy_cost_4);
					list.Add(texEnergyCost);
					break;
				case 5:
					texEnergyCost = LoadTextureFromResource(Art.energy_cost_5);
					list.Add(texEnergyCost);
					break;
				case 6:
					texEnergyCost = LoadTextureFromResource(Art.energy_cost_6);
					list.Add(texEnergyCost);
					break;
			}

			//Switch statement to set the bone texture to the right cost
			switch (boneCost)
			{
				case 1:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_1);
					list.Add(texBoneCcost);
					break;
				case 2:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_2);
					list.Add(texBoneCcost);
					break;
				case 3:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_3);
					list.Add(texBoneCcost);
					break;
				case 4:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_4);
					list.Add(texBoneCcost);
					break;
				case 5:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_5);
					list.Add(texBoneCcost);
					break;
				case 6:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_6);
					list.Add(texBoneCcost);
					break;
				case 7:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_7);
					list.Add(texBoneCcost);
					break;
				case 8:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_8);
					list.Add(texBoneCcost);
					break;
				case 9:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_9);
					list.Add(texBoneCcost);
					break;
				case 10:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_10);
					list.Add(texBoneCcost);
					break;
				case 11:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_11);
					list.Add(texBoneCcost);
					break;
				case 12:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_12);
					list.Add(texBoneCcost);
					break;
				case 13:
					texBoneCcost = LoadTextureFromResource(Art.bone_cost_13);
					list.Add(texBoneCcost);
					break;
			}

			switch (bloodCost)
			{
				case 1:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_1);
					list.Add(texBloodCost);
					break;
				case 2:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_2);
					list.Add(texBloodCost);
					break;
				case 3:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_3);
					list.Add(texBloodCost);
					break;
				case 4:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_4);
					list.Add(texBloodCost);
					break;
				case 5:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_5);
					list.Add(texBloodCost);
					break;
				case 6:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_6);
					list.Add(texBloodCost);
					break;
				case 7:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_7);
					list.Add(texBloodCost);
					break;
				case 8:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_8);
					list.Add(texBloodCost);
					break;
				case 9:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_9);
					list.Add(texBloodCost);
					break;
				case 10:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_10);
					list.Add(texBloodCost);
					break;
				case 11:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_11);
					list.Add(texBloodCost);
					break;
				case 12:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_12);
					list.Add(texBloodCost);
					break;
				case 13:
					texBloodCost = LoadTextureFromResource(Art.blood_cost_13);
					list.Add(texBloodCost);
					break;
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
			Texture2D finalTexture = CombineTextures(list, total, Art.empty_cost);

			//Convert the final texture to a sprite
			Sprite finalSprite = MakeSpriteFromTexture2D(finalTexture, true);

			return finalSprite;
		}


	}
}
