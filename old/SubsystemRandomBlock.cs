﻿using Engine;
using Engine.Audio;
using Engine.Graphics;
using Engine.Input;
using Engine.Media;
using Engine.Serialization;
using Game;
using GameEntitySystem;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TemplatesDatabase;

namespace Game
{
    public class SubsystemRandomBlock : Subsystem
    {
    	public PlayerData m_playerData;

        //当玩家第一次添加时执行
        public void OnPlayerFirstAdded(ComponentPlayer componentPlayer)
        {
            //清空衣服
            /*
            ComponentClothing componentClothing = componentPlayer.Entity.FindComponent<ComponentClothing>();
            if (componentClothing != null)
            {
                foreach (var slot in componentClothing.m_clothes.Keys)
                {
                    componentClothing.m_clothes[slot].Clear();
                }
            }*/
            //背包添加物品
            ComponentInventory componentInventory = componentPlayer.Entity.FindComponent<ComponentInventory>();
            if (componentInventory != null)
            {
            	foreach (var slot in componentInventory.m_slots)
                {
                	if (slot.Value == 0)
                	{
                		componentInventory.AddSlotItems(slotIndex: slot.Value,value: RandomBlock.Index, count: new System.Random().Next(10,20));
                		return;
                	}
                }
            }
        }

        public override void OnEntityAdded(Entity entity)
        {
            ComponentPlayer componentPlayer = entity.FindComponent<ComponentPlayer>();
            if (componentPlayer != null && m_playerData != null && m_playerData == componentPlayer.PlayerData)
            {
                OnPlayerFirstAdded(componentPlayer);
                m_playerData = null;
            }
        }

        public override void Load(ValuesDictionary valuesDictionary)
        {
            base.Load(valuesDictionary);
            base.Project.FindSubsystem<SubsystemPlayers>(true).PlayerAdded += delegate (PlayerData playerData)
            {
                m_playerData = playerData;
            };
        }
    }
}