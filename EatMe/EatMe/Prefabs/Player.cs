using EatMe.Components;
using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Prefabs
{
	public class Player : Prefab
	{
		private static readonly Player ThisPrefab = new Player{Tag = "Player"};


		public static Player GetPrefab()
		{
			return ThisPrefab;
		}

		public static Entity Instantiate(Vector2 position,string skinName)
		{
			Entity entity = new Entity();
			entity.AttachComponent(new Transform()
			{
				Position = position
			});
			entity.AttachComponent(new SpriteRenderer(string.Format("{0}{1}", Configuration.SpritesFolder ,skinName)) {DrawLayer = Configuration.PlayerMinDrawLayer});
			entity.AttachComponent(new CellScript()
			{
				Radius = Configuration.PlayerStartingRadius
			});

			entity.Tag = ThisPrefab.Tag;

			ThisPrefab.World.AddEntity(entity);

			return entity;
		}
	}
}
