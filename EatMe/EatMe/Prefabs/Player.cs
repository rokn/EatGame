using EatMe.Components;
using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Prefabs
{
	public class Player : Prefab
	{
		private static readonly Player Instance = new Player();

		public static Player GetInstance()
		{
			return Instance;
		}

		public static Entity Instantiate(Vector2 position,string skinName)
		{
			Entity entity = new Entity();
			entity.AttachComponent(new Transform()
			{
				Position = position
			});
			entity.AttachComponent(new SpriteRenderer(string.Format("Sprites\\{0}", skinName)) {DrawLayer = 1});
			entity.AttachComponent(new CellScript()
			{
				Radius = 30
			});

			entity.Tag = "Player";

			Instance.World.AddEntity(entity);

			return entity;
		}
	}
}
