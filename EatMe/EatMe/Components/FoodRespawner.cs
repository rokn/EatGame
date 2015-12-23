using EatMe.Prefabs;
using ECS;

namespace EatMe.Components
{
	public class FoodRespawner : Component
	{
		public override void Destroy()
		{
			Food.Instantiate();
		}
	}
}
