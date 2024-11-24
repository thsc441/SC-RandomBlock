using System;
using Engine;
using Engine.Graphics;

namespace Game
{
	class RandomBlock : CubeBlock
	{
		public const int Index = 260;
		
		public override void Initialize()
		{
    		DefaultCategory = "Items";//种类
    		InHandScale = (float)0.35;
        	FirstPersonScale = (float)0.4;
        	FirstPersonOffset = new Vector3((float)0.5, (float)-0.5, (float)-0.5);
        	DefaultDisplayName = "随机方块";//名称
        	MaxStacking = 2147483647;//最大堆叠
        	DefaultMeleePower = 2147483647;//攻击力
        	DefaultDropContent = Index;//掉落物
        	IsTransparent = true;//是否透明
        	DefaultMeleeHitProbability = 1f;//命中率
        	IsAimable = true;//是否可投掷
        	DefaultProjectilePower = 2147483647;//远程攻击力
        	DefaultDescription = DefaultDisplayName;//描述
        	Behaviors = "ThrowableBlockBehavior,RandomBlockBehavior";//方块行为 ThrowableBlockBehavior:投掷
        	//DestructionDebrisScale = 10f;//方块碎片大小
        	ProjectileDamping = 1f;//投掷速度衰减
        	CraftingId = "randomblock";
        	int num = Terrain.ExtractData(Index);
            num &= 0xF;
            num |= MathUtils.Clamp(2147483647, 0, 4095) << 4;//攻击力
    		base.Initialize();
		}
		
		public override void DrawBlock(PrimitivesRenderer3D primitivesRenderer,int value, Color color, float size, ref Matrix matrix,DrawBlockEnvironmentData environmentData)
        {
        	BlocksManager.DrawCubeBlock(primitivesRenderer, value, new Vector3(size), ref matrix, color, color, environmentData);
        }
    
        public override void GenerateTerrainVertices(BlockGeometryGenerator generator,TerrainGeometry geometry,int value, int x, int y, int z)
        {
        	generator.GenerateCubeVertices(this, value, x, y, z, Color.White, geometry.OpaqueSubsetsByFace);
        }
        
        public override int GetFaceTextureSlot(int face, int value)
		{
    		switch (face)
    		{
        		case 0: return new System.Random().Next(0,255);
        		case 1: return new System.Random().Next(0,255);
        		case 2: return new System.Random().Next(0,255);
        		case 3: return new System.Random().Next(0,255);
        		case 4: return new System.Random().Next(0,255);
        		case 5: return new System.Random().Next(0,255);
        		default:
        		{
            		//Log.Error(new ArgumentOutOfRangeException().ToString());
            		return 0;
        		}
    		}
		}
	}
}