using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine;
using System.Threading;
using static Engine.Storage;

namespace Game
{
	public class ModLoader_RandomBlockMod : ModLoader
	{
		public static ClothingBlock clothingBlocks;

		public override async void __ModInitialize()
		{
			Log.Information("随机方块Mod初始化中");
			ModsManager.RegisterHook("OnLoadingFinished", this);
			ModsManager.RegisterHook("ClothingProcessSlotItems", this);
		}

		public override void OnLoadingFinished(System.Collections.Generic.List<System.Action> actions)
		{
			Log.Information("[随机方块]检查更新中");
			CheckUpdate();
			Log.Information("初始化随机装备...");
			clothingBlocks = new ClothingBlock();
			clothingBlocks.Initialize();
			/*foreach (ClothingData cd in cb.m_clothingData){
        		cd.Mount = (value,cc) => {
        			Log.Information($"{cd.DisplayName}");
        		};
        		cd.Mount.Invoke(0,new ComponentClothing());
        	}*/
		}

		public override bool ClothingProcessSlotItems(ComponentPlayer componentPlayer, Block block, int slotIndex, int value, int count)
		{
			if (block.CanWear(value))
			{
				ClothingData clothingData = block.GetClothingData(value);
				//Log.Information($"Index : {clothingData.Index} {value} {clothingData.DisplayName}");
				switch (clothingData.Index)
				{
					case 38:
						break;
					case 39:
						break;
					case 40:
						break;
					case 41:
						break;
					default:
						return true;
				}
				//clothingData.Texture = block.GetClothingData(RandomClothing(clothingData.Slot)).Texture;//更改材质
				var list = new List<int>(componentPlayer.ComponentClothing.GetClothes(clothingData.Slot))
				{
					RandomClothing(clothingData.Slot)
				};
				componentPlayer.ComponentClothing.SetClothes(clothingData.Slot, list);
				new Thread(new ThreadStart(() =>
				{
					while (true)
					{
						if (componentPlayer.ComponentClothing.m_clothes[clothingData.Slot].Remove(value))
						{
							break;
						}
					}
				})).Start();
			}
			return true;
		}

		static async Task CheckUpdate()
		{
			HttpClient httpClient = new HttpClient();
			string updateurl = "https://thsc441.github.io/mod.csv";
			var response = await httpClient.GetAsync(updateurl);
			//Log.Information($"{response.EnsureSuccessStatusCode().ToString()}");
			string str = await response.Content.ReadAsStringAsync();
			String[] strings = str.Split(",");
			LoadingScreen.Info($"最新版本{strings[1]}");
			if (Convert.ToInt32(RandomBlockModInfo.Version) < Convert.ToInt32(strings[1]))
			{
				LoadingScreen.Info($"{RandomBlockModInfo.Name}有新版本，可去{strings[2]}更新");
				//new BusyDialog($"{RandomBlockModInfo.Name}有新版本",$"可前往{strings[2]}更新");
				/*
        		DialogsManager.ShowDialog(null, new MessageDialog($"{RandomBlockModInfo.Name}有新版本", $"可前往{strings[2]}更新，是否自动下载", LanguageControl.Ok, LanguageControl.Cancel, delegate (MessageDialogButton result){
        			if (result == MessageDialogButton.Button1)
        			{
        				try{
        					WebClient webClient = new WebClient();
        					webClient.DownloadFile(strings[2],ModsManager.path+$"{RandomBlockModInfo.Name}.scmod");
        					DialogsManager.ShowDialog(null,new MessageDialog("下载完成","自动更新模组",LanguageControl.Ok,LanguageControl.Cancel,null));
        					ModsManager.Initialize();
        				}catch(Exception ex){
        					//DialogsManager.ShowDialog(null,new MessageDialog(ex.ToString(),"","ok","no",null));
        					Log.Error(ex.ToString());
        					ex = ex.InnerException;
        				}
        			}
        		}));  //  展示一个新的消息对话框    这里的null可以输入一个委托，用于处理按下确定按钮时会发生的事件
        		*/
			}
			else
			{
				Log.Information("最新版本");
			}
		}

		public static int RandomClothing(ClothingSlot Slot)
		{
			int value = 203;
			ClothingData cd = new ClothingData();
			while ((cd = clothingBlocks.m_clothingData[new System.Random().Next(0, clothingBlocks.m_clothingData.Count - 1)]).Slot != Slot)
			{
				continue;
			}
			value = Terrain.MakeBlockValue(203, 0, ClothingBlock.SetClothingIndex(ClothingBlock.SetClothingColor(0, 0), cd.Index));
			//Log.Information($"Index : {cd.Index} {value} {cd.DisplayName}");
			return value;
		}
	}
}