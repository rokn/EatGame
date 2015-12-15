namespace ECS
{
	public abstract class Prefab
	{
		public EntityWorld World { get; private set; }

		public void AttachToWorld(EntityWorld world)
		{
			World = world;
		}
	}
}
