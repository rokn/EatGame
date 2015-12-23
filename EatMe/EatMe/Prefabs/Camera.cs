using EatMe.Components;
using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Prefabs
{
	public class Camera : Prefab
	{
		private static readonly Camera ThisPrefab = new Camera {Tag = "Camera"};

		public static Camera GetPrefab()
		{
			return ThisPrefab;
		}

		public static Entity Instantiate(int screenWidth, int screenHeight)
		{
			Entity entity = new Entity();
			entity.AttachComponent(new Transform());
			entity.AttachComponent(new OrtographicCamera(new Point(screenWidth, screenHeight)));

			entity.Tag = ThisPrefab.Tag;

			ThisPrefab.World.AddEntity(entity);

			return entity;
		}
	}
}
