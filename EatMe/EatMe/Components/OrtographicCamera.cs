using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Components
{
	public class OrtographicCamera : Component, IUpdateableComponent
	{
		private Transform _transform;
		private Point _screenSize;

		public OrtographicCamera(Point screenSize)
		{
			_screenSize = screenSize;
		}

		public Matrix TransformMatrix { get; private set; }

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();
			_transform.Position = new Vector2(_screenSize.X,_screenSize.Y);
		}

		public void Update(double deltaTime)
		{
			CalculateMatrix();
		}

		public void SmoothFollow(Transform target, float smoothTime, double deltaTime)
		{
			Vector2 goalPos = target.Position;
			_transform.Position = Vector2.SmoothStep(_transform.Position, goalPos, smoothTime * (float)deltaTime);
			
		}

		public void SmoothFollow(Transform target, float smoothTime, double deltaTime, Vector2 offset)
		{
			
			CalculateMatrix();
		}

		public void UpdateScreenDimensions(Point newScreenSize) => _screenSize = newScreenSize;


		/// <summary>
		/// Calculates the transform matrix of the camera
		/// based on the transform component and screen size
		/// </summary>
		private void CalculateMatrix()
		{
			TransformMatrix = Matrix.CreateTranslation(new Vector3(-_transform.Position.X, -_transform.Position.Y, 0)) *
										 Matrix.CreateRotationZ((float) _transform.Rotation) *
										 Matrix.CreateScale(new Vector3(_transform.Scale.X, _transform.Scale.Y, 1)) *
										 Matrix.CreateTranslation(new Vector3(_screenSize.X / 2, _screenSize.Y / 2, 0));
		}
	}
}
