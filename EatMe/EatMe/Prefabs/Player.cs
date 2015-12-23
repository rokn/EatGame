using EatMe.Components;
using EatMe.Common;
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

		public static Entity Instantiate(string skinName)
		{
			return Instantiate(HelperMethods.GetRandomPositionInBounds(), skinName);
		}

		public static Entity Instantiate(Vector2 poistion, string skinName)
		{
			Entity entity = new Entity();
			entity.AttachComponent(new Transform()
			{
				Position = poistion
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
