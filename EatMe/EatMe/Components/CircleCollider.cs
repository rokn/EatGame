using System;
using Microsoft.Xna.Framework;

namespace EatMe.Components
{
	public class CircleCollider : Collider
	{
		public float Radius
		{
			get
			{
				return _radius;
			}

			set
			{
				if (value < 0)
				{
					throw new InvalidOperationException("Radius must be non negative.");
				}

				_radius = value;
			}
		}
		
		private float _radius;

		public override bool CheckCollision(Collider other)
		{
			var collided = false;

			if (other is CircleCollider)
			{
				var distance = Vector2.Distance(Entity.GetComponent<Transform>().Position + Offset,
					other.Entity.GetComponent<Transform>().Position + other.Offset);

				if (distance <= Radius + ((CircleCollider) other).Radius)
				{
					OnCollideEvent(this, other);
					collided = true;
				}
			}

			base.CheckCollision(other);

			return collided;
		}
	}
}
