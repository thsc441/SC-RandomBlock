using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace Game
{
    public class RandomBlockModEntity : ModEntity
    {
    	public RandomBlockModEntity(){
    		Log.Information("fuck");
    	}
    	
        //public override void LoadClo(ClothingBlock block, ref XElement xElement){
        	//Log.Information("初始化随机装备...");
        	/*foreach (var cdd in block.m_clothingData){
        		int Index = cdd.Key;
        		ClothingData cd = cdd.Value;
        		Log.Information($"Index : {Index} 名字 : {cd.DisplayName}");
        		switch(cd.Index){
        			case 38:
        				cd.Mount = delegate(int value,ComponentClothing cc){
        					var list = new List<int>(cc.GetClothes(cd.Slot))
                			{
                    			value
                			};
                			cc.SetClothes(cd.Slot, list);
        				};
        				break;
        		}
        	}*/
        	//base.LoadClo(block,ref xElement);
        //}
    }
}