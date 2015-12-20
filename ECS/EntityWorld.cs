using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	public class EntityWorld
	{
		#region Fields

		private readonly List<Entity> _entities;
		private readonly List<Entity> _removeList;

		#endregion

		#region Constructors

		public EntityWorld()
		{
			_entities = new List<Entity>();
			_removeList = new List<Entity>();
		}

		#endregion

		#region Entity Managment

		public void AddEntity(Entity entity)
		{
			_entities.Add(entity);

			foreach (var component in entity.GetComponents())
			{
				component.World = this;
			}

			entity.World = this;
		}

		//TODO: Make add/remove entity event

		public Entity GetEntityWithTag(string tag)
		{
			return _entities.FirstOrDefault(entity => tag == entity.Tag);
		}

		public IEnumerable<Entity> GetEntitiesWithTag(string tag)
		{
			return _entities.Where(entity => tag == entity.Tag).ToList();
		}

		public Entity GetEntityWithComponent<T>()
			where T : Component
		{
			return _entities.FirstOrDefault(entity => entity.HasComponent<T>());
		}

		public IEnumerable<Entity> GetEntitiesWithComponent<T>()
			where T : Component
		{
			return _entities.Where(entity => entity.HasComponent<T>()).ToList();
		}

		public void RemoveEntity(Entity entity)
		{
			_removeList.Add(entity);
		}

		#endregion

		#region Main Functionality

		public void Start()
		{
			foreach (var component in _entities.SelectMany(entity => entity.GetComponents()))
			{
				component.Start();
			}
		}

		public void Update(double deltaTime)
		{
			foreach (IUpdateableComponent
				component in _entities
					.SelectMany(entity => entity.GetComponents()
						.OfType<IUpdateableComponent>()))
			{
				component.Update(deltaTime);
			}

			UpdateRemoveList();
		}

		public void Draw()
		{
			var query = _entities
				.SelectMany(entity => entity.GetComponents()
					.OfType<IDrawableComponent>()).ToList();

			var ordered = query.OrderBy(component => component.DrawLayer);

			foreach (IDrawableComponent component in ordered)
			{
				component.Draw();
			}
		}

		public void GuiDraw()
		{
			var query = _entities
				.SelectMany(entity => entity.GetComponents()
					.OfType<IGuiDrawableComponent>()).ToList();

			var ordered = query.OrderBy(component => component.GuiDrawLayer);

			foreach (IGuiDrawableComponent component in ordered)
			{
				component.GuiDraw();
			}
		}

		public void Destroy()
		{
			foreach (var entity in _entities)
			{
				entity.Destroy();
			}

			UpdateRemoveList();
		}

		#endregion

		private void UpdateRemoveList()
		{
			foreach(var entity in _removeList)
			{
				_entities.Remove(entity);
			}

			_removeList.Clear();
		}
	}
}