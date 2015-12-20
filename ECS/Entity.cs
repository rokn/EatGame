using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	public class Entity
	{
		#region Fields

		private readonly Dictionary<string, Component> _components;

		#endregion

		#region Constructors

		static Entity()
		{
			IdCount = 0;
		}

		/// <summary>
		///     Creates a new entity with no components
		/// </summary>
		public Entity()
		{
			_components = new Dictionary<string, Component>();
			Id = IdCount++;
			Tag = "Untagged";
		}

		#endregion

		#region Properties

		public static uint IdCount { get; private set; }
		public uint Id { get; }
		public string Tag { get; set; }
		public EntityWorld World { get; set; }

		#endregion

		#region Component Managment

		/// <summary>
		///     Attaches a new componenet to the entity
		/// </summary>			
		/// <param name="component">The component to attach</param>
		/// <returns>Returns true if attachment is succesful</returns>
		public bool AttachComponent(Component component)
		{
			var typeName = component.GetType().ToString();

			if (_components.ContainsKey(typeName))
				return false;

			component.Entity = this;
			component.World = World;
			_components.Add(typeName, component);
			return true;
		}

		/// <summary>
		///     Removes all components from the entity
		/// </summary>
		public void ClearComponents()
		{
			_components.Clear();
		}

		/// <summary>
		///     Gets a compnenent from the entity
		/// </summary>
		/// <typeparam name="T">The type of the component to get</typeparam>
		/// <returns>The compenent</returns>
		public T GetComponent<T>()
			where T : Component
		{
			var type = typeof (T);

			//			return _components.ContainsKey(type.ToString()) ? _components[type.ToString()] as T : null;

			return (from kvp
				in _components
				where kvp.Key == type.ToString() || kvp.Value.GetType().IsSubclassOf(type)
				select kvp.Value).FirstOrDefault() as T;
		}

		public bool HasComponent<T>()
			where T : Component
		{
			var type = typeof (T);

			return _components.Any(kvp => type.ToString() == kvp.Key || kvp.Value.GetType().IsSubclassOf(type));
		}

		public List<Component> GetComponents()
		{
			return _components.Values.ToList();
		}

		#endregion

		public override int GetHashCode()
		{
			unchecked
			{
				var hash = 17;
				hash = (int) (hash*23 + Id);
				return hash;
			}
		}

		public void Destroy()
		{
			foreach (var kvp in _components)
			{
				kvp.Value.Destroy();
			}

			World.RemoveEntity(this);
		}
	}
}