using EatMe.Components;
using EatMe.Common;
using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Prefabs
{
	public class Food : Prefab
	{
		public static Food ThisPrefab = new Food {Tag = "Food"};

		public static int FoodInstances = 0;

		public static Food GetPrefab()
		{
			return ThisPrefab;
		}

		public static Entity Instantiate()
		{
			return Instantiate(HelperMethods.GetRandomPositionInBounds());
		}


		public static Entity Instantiate(Vector2 position)
		{
			FoodInstances++;

			Entity entity = new Entity();
			entity.AttachComponent(new Transform()
			{
				Position = position
			});

			entity.AttachComponent(new SpriteRenderer(Configuration.SpritesFolder+"Food")
			{
				Color = HelperMethods.RandomColor(),
				DrawLayer = 0
			});

			entity.Tag = ThisPrefab.Tag;

			var imageSize = entity.GetComponent<SpriteRenderer>().Width;

			entity.AttachComponent(new CircleCollider() { Radius = imageSize / 2 });
			entity.AttachComponent(new FoodRespawner());

			ThisPrefab.World.AddEntity(entity);

			return entity;
		}
	}
}
