using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Components
{
	public class Collider : Component, IUpdateableComponent
	{
		public bool IsDynamic{ get; set; }

		public bool Checked { get; protected set; }

		public delegate void CollideEventHandler(Collider myCollider, Collider other);

		public event CollideEventHandler CollideEvent;

		public Vector2 Offset { get; set; }

		protected Transform EntityTransform;

		public Collider()
		{
			IsDynamic = false;
			Offset = new Vector2();
		}

		public override void Start()
		{
			EntityTransform = Entity.GetComponent<Transform>();
		}

		public virtual void Update(double deltaTime)
		{
			Checked = false;
		}

		public virtual bool CheckCollision(Collider other)
		{
			Checked = true;
			return true;
		}

		protected void OnCollideEvent(Collider myCollider, Collider other)
		{
			CollideEvent?.Invoke(myCollider, other);
		}
	}
}
