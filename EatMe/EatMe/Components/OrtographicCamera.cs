using EatMe.Common;
using ECS;
using Microsoft.Xna.Framework;

namespace EatMe.Components
{
	public class OrtographicCamera : Component, IUpdateableComponent
	{
		private Transform _transform;
		private Point _screenSize;
		public Rectangle _viewRectangle;

		public OrtographicCamera(Point screenSize)
		{
			_screenSize = screenSize;
			_viewRectangle = new Rectangle(0,0,screenSize.X,screenSize.Y);
		}

		public Matrix TransformMatrix { get; private set; }

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();
			_transform.Position = new Vector2();
			_viewRectangle.UpdateViaVector(_transform.Position);
		}

		public void Update(double deltaTime)
		{
			CalculateMatrix();
			_viewRectangle = _viewRectangle.UpdateViaVector(TransformScreenCoordinates(new Vector2()));
			_viewRectangle.Width = (int)(_screenSize.X * 1/_transform.Scale.X);
			_viewRectangle.Height = (int)(_screenSize.Y * 1/_transform.Scale.Y);
		}

		/// <summary>
		/// Move the camera by a given amount
		/// </summary>
		/// <param name="amount">By how much to move the camera</param>
		public void Move(Vector2 amount)
		{
			_transform.Position += amount;
		}

		/// <summary>
		/// Update the dimensions of the camera
		/// </summary>
		/// <param name="newScreenSize"></param>
		public void UpdateScreenDimensions(Point newScreenSize) => _screenSize = newScreenSize;

		/// <summary>
		/// Transform screen coords into game coords
		/// </summary>
		/// <param name="screenPos">The coordinates on the screen</param>
		/// <returns>Coordinates in the game</returns>
		public Vector2 TransformScreenCoordinates(Vector2 screenPos)
		{
			return Vector2.Transform(screenPos, Matrix.Invert(TransformMatrix));
		}

		public bool IsInView(Vector2 point)
		{
			return _viewRectangle.Contains(point);
		}

		public bool IsInView(Rectangle rect)
		{
			return _viewRectangle.Intersects(rect);
		}

		/// <summary>
		/// Calculates the transform matrix of the camera
		/// </summary>
		private void CalculateMatrix()
		{
			if (_transform == null) return;

			TransformMatrix = Matrix.CreateTranslation(new Vector3(-_transform.Position.X, -_transform.Position.Y, 0)) *
										 Matrix.CreateRotationZ((float) _transform.Rotation) *
										 Matrix.CreateScale(new Vector3(_transform.Scale.X, _transform.Scale.Y, 1)) *
										 Matrix.CreateTranslation(new Vector3(_screenSize.X/2, _screenSize.Y/2, 0));
		}
	}
}
