using EatMe.Components;
using ECS;

namespace EatMe.Prefabs
{
	public class Player : Prefab
	{
		private static readonly Player Instance = new Player();

		public static Player GetInstance()
		{
			return Instance;
		}

		public static Entity Instantiate(string skinName)
		{
			Entity entity = new Entity();
			entity.AttachComponent(new Transform());
			entity.AttachComponent(new SpriteRenderer(string.Format("Sprites\\{0}", skinName)) {DrawLayer = 1});

			Instance.World.AddEntity(entity);

			return entity;
		}
	}
}
