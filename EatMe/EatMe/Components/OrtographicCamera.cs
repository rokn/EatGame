using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;

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
			_transform.Position = new Vector2();
		}

		public void Update(double deltaTime)
		{
			CalculateMatrix();
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
		/// Transform the screen coordinates into game coordinates
		/// </summary>
		/// <param name="screenPos">The coordinates on the screen</param>
		/// <returns>Coordinates in the game</returns>
		public Vector2 TransformScreenCoordinates(Vector2 screenPos)
		{
			return Vector2.Transform(screenPos, Matrix.Invert(TransformMatrix));
		}

		/// <summary>
		/// Calculates the transform matrix of the camera
		/// based on the transform component and screen size
		/// </summary>
		private void CalculateMatrix()
		{
			TransformMatrix = Matrix.CreateTranslation(new Vector3(-_transform.Position.X, -_transform.Position.Y, 0)) *
										 Matrix.CreateRotationZ((float) _transform.Rotation) *
										 Matrix.CreateScale(new Vector3(_transform.Scale.X, _transform.Scale.Y, 1)) *
										 Matrix.CreateTranslation(new Vector3(_screenSize.X/2, _screenSize.Y/2, 0));
		}
	}
}
