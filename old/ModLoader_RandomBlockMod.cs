using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine;
using static Engine.Storage;

namespace Game
{
    public class ModLoader_RandomBlockMod : ModLoader
    {
        public override async void __ModInitialize(){
        	//Log.Information("随机方块Mod初始化中");
        	ModsManager.ModListAll.RemoveAll(x => x.modInfo.Name == RandomBlockModInfo.Name);
        	ModsManager.RegisterHook("OnLoadingFinished",this);
        }
        
        public override void OnLoadingFinished(System.Collections.Generic.List<System.Action> actions)
        {
        	Log.Information("[随机方块]检查更新中");
        	CheckUpdate();
        }
        
        static async Task CheckUpdate()
        {
        	HttpClient httpClient = new HttpClient();
        	string updateurl = "https://thsc441.github.io/mod.csv";
        	var response = await httpClient.GetAsync(updateurl);
        	//Log.Information($"{response.EnsureSuccessStatusCode().ToString()}");
        	string str = await response.Content.ReadAsStringAsync();
        	String[] strings = str.Split(",");
        	Log.Information($"最新版本{strings[1]}");
        	if(Convert.ToInt32(RandomBlockModInfo.Version) < Convert.ToInt32(strings[1]))
        	{
        		Log.Information($"{RandomBlockModInfo.Name}有新版本，可去{strings[2]}更新");
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
        	}else{
        		Log.Information("最新版本");
        	}
        }
    }
}