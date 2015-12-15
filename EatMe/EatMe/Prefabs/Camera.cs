using EatMe.Components;
using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Prefabs
{
	public class Camera : Prefab
	{
		private static readonly Camera Instance = new Camera();

		public static Camera GetInstance()
		{
			return Instance;
		}

		public static Entity Instantiate(int screenWidth, int screenHeight)
		{
			Entity entity = new Entity();
			entity.AttachComponent(new Transform());
			entity.AttachComponent(new OrtographicCamera(new Point(screenWidth, screenHeight)));

			Instance.World.AddEntity(entity);

			return entity;
		}
	}
}
