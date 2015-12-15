using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	public class EntityWorld
	{
		private readonly List<Entity> _entities;

		public EntityWorld()
		{
			_entities = new List<Entity>();
		}

		public void AddEntity(Entity entity)
		{
			_entities.Add(entity);
			entity.World = this;
		}

		public IEnumerable<Entity> GetEntitiesWithTag(string tag)
		{
			return _entities.Where(entity => tag == entity.Tag).ToList();
		}

		public Entity GetEntityWithTag(string tag)
		{
			return _entities.FirstOrDefault(entity => tag == entity.Tag);
		}

		public void Start()
		{
			foreach (var component in _entities.SelectMany(entity => entity.GetComponents()))
			{
				component.Start();
			}
		}

		public void Update(double deltaTime)
		{
			foreach (var component in _entities.SelectMany(entity => entity.GetComponents()))
			{
				component.Update(deltaTime);
			}
		}

		public void Draw()
		{
			foreach (var component in _entities.SelectMany(entity => entity.GetComponents()).OrderBy(comp => comp.DrawLayer))
			{
				component.Draw();
			}
		}

		public void Destroy()
		{
			foreach (var entity in _entities)
			{
				entity.Destroy();
			}
		}
	}
}