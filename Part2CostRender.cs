using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;

using Art = RenderFixMaybe.Resources.costs;

namespace RenderFixMaybe
{
	public class Part2CostRender
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

		public static Sprite MakeSpriteFromTexture2D(Texture2D texture)
		{
			Sprite sprite;
			float Ytest = 0.75f;
			float Xtest = 0.85f;
			float ppu = 100.0f;
			Vector2 vec = new Vector2(Xtest, Ytest);
			sprite = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), vec, ppu);
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


		public static Sprite Part2SpriteFinal(CardInfo card)
		{
			List<Vector2Int> pixelCost = new List<Vector2Int>
			{
				new Vector2Int(1, 0),
				new Vector2Int(8, 0)
			};

			Texture2D texBloodCost = LoadTextureFromResource(Art.pixel_blank);
			Texture2D texBoneCcost = LoadTextureFromResource(Art.pixel_blank);
			Texture2D texEnergyCost = LoadTextureFromResource(Art.pixel_blank);
			Texture2D textGemCost = LoadTextureFromResource(Art.pixel_blank);
			Texture2D textCost = LoadTextureFromResource(Art.pixel_blank);
			//A list to hold the textures (important later, to combine them all)
			List<Texture2D> masterList = new List<Texture2D>();

			int bloodCost = card.BloodCost;
			int boneCost = card.BonesCost;
			int energyCost = card.energyCost;
			int moxCost = card.gemsCost.Count;

			if (bloodCost > 0)
			{
				List<Texture2D> list = new List<Texture2D>();
				texBloodCost = LoadTextureFromResource(Art.pixel_blood);
				list.Add(texBloodCost);
				switch (bloodCost)
				{
					case 1:
						textCost = LoadTextureFromResource(Art.pixel_1);
						list.Add(textCost);
						break;
					case 2:
						textCost = LoadTextureFromResource(Art.pixel_2);
						list.Add(textCost);
						break;
					case 3:
						textCost = LoadTextureFromResource(Art.pixel_3);
						list.Add(textCost);
						break;
					case 4:
						textCost = LoadTextureFromResource(Art.pixel_4);
						list.Add(textCost);
						break;
					case 5:
						textCost = LoadTextureFromResource(Art.pixel_5);
						list.Add(textCost);
						break;
					case 6:
						textCost = LoadTextureFromResource(Art.pixel_6);
						list.Add(textCost);
						break;
					case 7:
						textCost = LoadTextureFromResource(Art.pixel_7);
						list.Add(textCost);
						break;
					case 8:
						textCost = LoadTextureFromResource(Art.pixel_8);
						list.Add(textCost);
						break;
					case 9:
						textCost = LoadTextureFromResource(Art.pixel_9);
						list.Add(textCost);
						break;
					case 10:
						textCost = LoadTextureFromResource(Art.pixel_10);
						list.Add(textCost);
						break;
					case 11:
						textCost = LoadTextureFromResource(Art.pixel_11);
						list.Add(textCost);
						break;
					case 12:
						textCost = LoadTextureFromResource(Art.pixel_12);
						list.Add(textCost);
						break;
					case 13:
						textCost = LoadTextureFromResource(Art.pixel_13);
						list.Add(textCost);
						break;
					case 14:
						textCost = LoadTextureFromResource(Art.pixel_14);
						list.Add(textCost);
						break;
					case 15:
						textCost = LoadTextureFromResource(Art.pixel_15);
						list.Add(textCost);
						break;
				}
				Texture2D finalBloodTexture = CombineTextures(list, pixelCost, Art.pixel_blank);
				masterList.Add(finalBloodTexture);

			}
			if (boneCost > 0)
			{
				List<Texture2D> list = new List<Texture2D>();
				texBoneCcost = LoadTextureFromResource(Art.pixel_bone);
				list.Add(texBoneCcost);
				switch (boneCost)
				{
					case 1:
						textCost = LoadTextureFromResource(Art.pixel_1);
						list.Add(textCost);
						break;
					case 2:
						textCost = LoadTextureFromResource(Art.pixel_2);
						list.Add(textCost);
						break;
					case 3:
						textCost = LoadTextureFromResource(Art.pixel_3);
						list.Add(textCost);
						break;
					case 4:
						textCost = LoadTextureFromResource(Art.pixel_4);
						list.Add(textCost);
						break;
					case 5:
						textCost = LoadTextureFromResource(Art.pixel_5);
						list.Add(textCost);
						break;
					case 6:
						textCost = LoadTextureFromResource(Art.pixel_6);
						list.Add(textCost);
						break;
					case 7:
						textCost = LoadTextureFromResource(Art.pixel_7);
						list.Add(textCost);
						break;
					case 8:
						textCost = LoadTextureFromResource(Art.pixel_8);
						list.Add(textCost);
						break;
					case 9:
						textCost = LoadTextureFromResource(Art.pixel_9);
						list.Add(textCost);
						break;
					case 10:
						textCost = LoadTextureFromResource(Art.pixel_10);
						list.Add(textCost);
						break;
					case 11:
						textCost = LoadTextureFromResource(Art.pixel_11);
						list.Add(textCost);
						break;
					case 12:
						textCost = LoadTextureFromResource(Art.pixel_12);
						list.Add(textCost);
						break;
					case 13:
						textCost = LoadTextureFromResource(Art.pixel_13);
						list.Add(textCost);
						break;
					case 14:
						textCost = LoadTextureFromResource(Art.pixel_14);
						list.Add(textCost);
						break;
					case 15:
						textCost = LoadTextureFromResource(Art.pixel_15);
						list.Add(textCost);
						break;
				}
				Texture2D finalBoneTexture = CombineTextures(list, pixelCost, Art.pixel_blank);
				masterList.Add(finalBoneTexture);


			}
			if (energyCost > 0)
			{
				List<Texture2D> list = new List<Texture2D>();
				texEnergyCost = LoadTextureFromResource(Art.pixel_energy);
				list.Add(texEnergyCost);
				switch (energyCost)
				{
					case 1:
						textCost = LoadTextureFromResource(Art.pixel_1);
						list.Add(textCost);
						break;
					case 2:
						textCost = LoadTextureFromResource(Art.pixel_2);
						list.Add(textCost);
						break;
					case 3:
						textCost = LoadTextureFromResource(Art.pixel_3);
						list.Add(textCost);
						break;
					case 4:
						textCost = LoadTextureFromResource(Art.pixel_4);
						list.Add(textCost);
						break;
					case 5:
						textCost = LoadTextureFromResource(Art.pixel_5);
						list.Add(textCost);
						break;
					case 6:
						textCost = LoadTextureFromResource(Art.pixel_6);
						list.Add(textCost);
						break;
					case 7:
						textCost = LoadTextureFromResource(Art.pixel_7);
						list.Add(textCost);
						break;
					case 8:
						textCost = LoadTextureFromResource(Art.pixel_8);
						list.Add(textCost);
						break;
					case 9:
						textCost = LoadTextureFromResource(Art.pixel_9);
						list.Add(textCost);
						break;
					case 10:
						textCost = LoadTextureFromResource(Art.pixel_10);
						list.Add(textCost);
						break;
					case 11:
						textCost = LoadTextureFromResource(Art.pixel_11);
						list.Add(textCost);
						break;
					case 12:
						textCost = LoadTextureFromResource(Art.pixel_12);
						list.Add(textCost);
						break;
					case 13:
						textCost = LoadTextureFromResource(Art.pixel_13);
						list.Add(textCost);
						break;
					case 14:
						textCost = LoadTextureFromResource(Art.pixel_14);
						list.Add(textCost);
						break;
					case 15:
						textCost = LoadTextureFromResource(Art.pixel_15);
						list.Add(textCost);
						break;
				}
				Texture2D finalEnergyTexture = CombineTextures(list, pixelCost, Art.pixel_blank);
				masterList.Add(finalEnergyTexture);

			}
			if (moxCost > 0)
			{
				List<Texture2D> gemCost = new List<Texture2D>();
				//load up the mox textures as "empty"
				Texture2D orange = LoadTextureFromResource(Art.pixel_mox_empty);
				Texture2D blue = LoadTextureFromResource(Art.pixel_mox_empty);
				Texture2D green = LoadTextureFromResource(Art.pixel_mox_empty);

				//If a card has a green mox, set the green mox
				if (card.GemsCost.Contains(GemType.Green))
				{
					green = LoadTextureFromResource(Art.pixel_mox_green);
					gemCost.Add(green);
				}
				//If a card has a green mox, set the Orange mox
				if (card.GemsCost.Contains(GemType.Orange))
				{
					orange = LoadTextureFromResource(Art.pixel_mox_red);
					gemCost.Add(orange);
				}
				//If a card has a green mox, set the Blue mox
				if (card.GemsCost.Contains(GemType.Blue))
				{
					blue = LoadTextureFromResource(Art.pixel_mox_blue);
					gemCost.Add(blue);
				}
				var gemCounting = gemCost.Count;
				var gemTotal = new List<Vector2Int>();
				switch (gemCounting)
				{
					case 1:
						gemTotal = new List<Vector2Int>
						{
							new Vector2Int(1, 0)
						};
						break;
					case 2:
						gemTotal = new List<Vector2Int>
						{
							new Vector2Int(1, 0),
							new Vector2Int(9, 0)
						};
						break;
					case 3:
						gemTotal = new List<Vector2Int>
						{
							new Vector2Int(1, 0),
							new Vector2Int(9, 0),
							new Vector2Int(17, 0)
						};
						break;
				}

				Texture2D finalMoxTexture = CombineTextures(gemCost, gemTotal, Art.pixel_blank);

				masterList.Add(finalMoxTexture);
			}

			var counting = masterList.Count;
			var total = new List<Vector2Int>();
			switch (counting)
			{
				case 0:
					masterList.Add(texBloodCost);
					total = new List<Vector2Int>
					{
						new Vector2Int(0, 0)
					};
					break;
				case 1:
					total = new List<Vector2Int>
					{
						new Vector2Int(0, 24)
					};
					break;
				case 2:
					total = new List<Vector2Int>
					{
						new Vector2Int(0, 16),
						new Vector2Int(0, 24)
					};
					break;
				case 3:
					total = new List<Vector2Int>
					{
						new Vector2Int(0, 8),
						new Vector2Int(0, 16),
						new Vector2Int(0, 24)
					};
					break;
				case 4:
					total = new List<Vector2Int>
					{
						new Vector2Int(0, 0),
						new Vector2Int(0, 8),
						new Vector2Int(0, 16),
						new Vector2Int(0, 24)
					};
					break;
			}

			//Combine all the textures from the list into one texture
			Texture2D finalTexture = CombineTextures(masterList, total, Art.pixel_base);

			//Convert the final texture to a sprite
			Sprite finalSprite = MakeSpriteFromTexture2D(finalTexture);
			return finalSprite;
		}
	}
}
