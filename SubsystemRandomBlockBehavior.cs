using System;
using Engine;
using GameEntitySystem;
using Engine.Graphics;

namespace Game
{
    public class SubsystemRandomBlockBehavior : SubsystemBlockBehavior
    {
    	public SubsystemTerrain m_SubsystemTerrain;
    	
    	public SubsystemPickables m_SubsystemPickables;
    	
    	//public ComponentInventory m_ComponentInventory;
    	
    	public ComponentPlayer m_ComponentPlayer;
    	
    	public static string fName = "SubsystemRandomBlockBehavior";
    	
    	public override int[] HandledBlocks => new int[0];
    	
        public override void OnBlockAdded(int value, int oldValue, int x, int y, int z)
        {
        	Block block = BlocksManager.Blocks[value];
        	//Log.Information($"方块{blockname}被放置");
        	int id = new System.Random().Next(0,1023);
        	Block block1 = BlocksManager.Blocks[id];
        	if (block1 != BlocksManager.Blocks[0]){
        		if (block1.IsPlaceable)
        		{
        			m_SubsystemTerrain.ChangeCell(x,y,z,id);
        			//Log.Information($"随机出了方块 id:{id}");
        		}else{
        			m_SubsystemTerrain.DestroyCell((int)Math.Ceiling(block.GetRequiredToolLevel(value)),x,y,z,0,true,false);
        			Vector3 position = new Vector3(x, y, z) + new Vector3(0.5f);
                	m_SubsystemPickables.AddPickable(id, 1, position, null, null);
                	//Log.Information($"随机出了不可放置的物品，生成掉落物 id:{id}");
        			/*
        			ComponentInventory componentInventory = m_ComponentPlayer.Entity.FindComponent<ComponentInventory>();
          			componentInventory.AddSlotItems(slotIndex : 0,value : id, count : 1);
          			*/
        		}
        	}else{
        		m_SubsystemTerrain.DestroyCell((int)Math.Ceiling(block.GetRequiredToolLevel(value)),x,y,z,0,true,true);
        		m_SubsystemTerrain.ChangeCell(x,y,z,value);
        		//Log.Information($"随机出的物品不存在 id:{id}");
        	}
        }
        
        public override void Load(TemplatesDatabase.ValuesDictionary valuesDictionary)
        {
        	m_SubsystemTerrain = Project.FindSubsystem<SubsystemTerrain>(throwOnError : true);
        	m_SubsystemPickables = Project.FindSubsystem<SubsystemPickables>(throwOnError: true);
        	//m_ComponentPlayer = Entity.FindComponent<ComponentPlayer>(throwOnError : true);
            base.Load(valuesDictionary);
        }
    }
}