using EatMe.Components;
using EatMe.UnifiedClasses;
using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Prefabs
{
	public class Food : Prefab
	{
		public static Food Instance = new Food();

		public static int FoodInstances = 0;

		public static Food GetInstance()
		{
			return Instance;
		}

		public static Entity Instantiate(Vector2 position)
		{
			FoodInstances++;

			Entity entity = new Entity();
			entity.AttachComponent(new Transform()
			{
				Position = position
			});

			entity.AttachComponent(new SpriteRenderer("Sprites\\Food")
			{
				Color = HelperMethods.RandomColor(),
				DrawLayer = 0
			});

			entity.Tag = "Food";

			var imageSize = entity.GetComponent<SpriteRenderer>().Width;

			entity.AttachComponent(new CircleCollider() { Radius = imageSize / 2 });

			Instance.World.AddEntity(entity);

			return entity;
		}
	}
}
