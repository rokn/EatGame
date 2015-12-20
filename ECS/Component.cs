namespace ECS
{

	#region Component Interfaces

	public interface IUpdateableComponent
	{
		void Update(double deltaTime);
	}

	public interface IDrawableComponent
	{
		int DrawLayer { get; set; }
		void Draw();
	}

	public interface IGuiDrawableComponent
	{
		int GuiDrawLayer { get; set; }
		void GuiDraw();
	}

	#endregion

	public class Component
	{
		#region Properties

		public Entity Entity { get; set; }
		public EntityWorld World { get; set; }

		#endregion

		#region Main functions

		public virtual void Start()
		{
		}

		public virtual void Destroy()
		{
		}

		#endregion

		#region Equality functions

		public override bool Equals(object obj)
		{
			Component other = obj as Component;

			if (other == null)
				return false;

			return Entity.Equals(other.Entity) && GetType() == obj.GetType();
		}

		public override int GetHashCode()
		{
			var hash = 17;
			hash = hash*23 + GetType().GetHashCode();
			hash = hash*23 + Entity.GetHashCode();
			return hash;
		}

		#endregion

	}
}
