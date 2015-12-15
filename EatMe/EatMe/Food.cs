using EatMe.Components;
using ECS;
using Microsoft.Xna.Framework;

namespace EatMe
{
	public class Food : Prefab
	{
		public static Food Instance = new Food();

		public static Food GetInstance()
		{
			return Instance;
		}

		public static Entity Instantiate(Vector2 position)
		{
			Entity entity = new Entity();
			entity.AttachComponent(new Transform()
			{
				Position = position
			});

			entity.AttachComponent(new SpriteRenderer("Sprites\\Food")
			{
				Color = HelperMethods.RandomColor()
			});

			Instance.World.AddEntity(entity);

			return entity;
		}
	}
}
