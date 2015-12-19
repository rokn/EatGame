using System;
using EatMe.Prefabs;
using ECS;

namespace EatMe.Components
{
	public class CellScript : Component, IUpdateableComponent
	{

		public float Radius
		{
			get { return _radius; }

			set
			{
				if(value <= 0)
					throw new InvalidOperationException("Radius must be positive.");

				_radius = value;

				UpdateRadius();
			}
		}

		private Transform _transform;
		private float _radius;
		private bool _radiusIsUpdated;

		public CellScript()
		{
			_radiusIsUpdated = false;
		}

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();

			CircleCollider collider = new CircleCollider() { Radius = _radius };
			collider.CollideEvent += FoodCollision;
			collider.CollideEvent += CellCollision;
			collider.IsDynamic = true;

			Entity.AttachComponent(collider);
		}

		public void Update(double deltaTime)
		{
		
			if(!_radiusIsUpdated)
				UpdateRadius();
		}

		private void UpdateRadius()
		{
			if(Entity == null || _transform == null)
				return;

			SpriteRenderer renderer = Entity.GetComponent<SpriteRenderer>();

			if(renderer == null)
			{
				_radiusIsUpdated = false;
				return;
			}

			var imageSize = renderer.Width;

			_transform.SetScale(_radius / (imageSize / 2));
			Entity.GetComponent<CircleCollider>().Radius = _radius;
			_radiusIsUpdated = true;
		}

		private void FoodCollision(Collider myCollider, Collider other)
		{
			if(other.Entity.Tag != "Food")
				return;

			Radius += 10 / Radius;

			//			Entity.GetComponent<SpriteRenderer>().Color = other.Entity.GetComponent<SpriteRenderer>().Color;
			other.Entity.Destroy();
			Food.FoodInstances--;
		}

		private void CellCollision(Collider myCollider, Collider other)
		{
			if(other.Entity.Tag != "Player")
				return;

			var circleCollider = other as CircleCollider;
			if (!(Radius - circleCollider?.Radius > 10)) return;

			Radius += circleCollider.Radius/10;
			other.Entity.Destroy();

			//			Entity.GetComponent<SpriteRenderer>().Color = other.Entity.GetComponent<SpriteRenderer>().Color;
		}
	}
}
