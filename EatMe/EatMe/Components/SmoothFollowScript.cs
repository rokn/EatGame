using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Components
{
	public class SmoothFollowScript : Component, IUpdateableComponent
	{
		public bool IsFollowing { get; set; }
		public Transform Target { get; set; }
		public Vector2 Offset { get; set; }
		public float SmoothTime { get; set; }

		private Transform _transform;

		public SmoothFollowScript()
		{
			Target = null;
			IsFollowing = false;
		}

		public SmoothFollowScript(Transform target)
		{
			Target = target;
			IsFollowing = true;
		}

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();
		}

		public void Update(double deltaTime)
		{
			if (!IsFollowing || Target == null || _transform == null) return;


			Vector2 goalPos = Target.Position + Offset;
			_transform.Position = Vector2.SmoothStep(_transform.Position, goalPos, SmoothTime * (float)deltaTime);
		}
	}
}
