using EatMe.Prefabs;
using EatMe.UnifiedClasses;
using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EatMe.Components
{
	public class PlayerController : Component, IUpdateableComponent, IGuiDrawableComponent
	{
		public const float BaseMovementSpeed = 400;
		public int GuiDrawLayer{ get; set; }

		public float MovementSpeed { get; set; }

		private Transform _transform;
		private SmoothFollowScript _followScript;

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();
			_followScript = Entity.GetComponent<SmoothFollowScript>();
		}

		public void Update(double deltaTime)
		{
			MovementSpeed = BaseMovementSpeed - Entity.GetComponent<CellScript>().Radius;

			Vector2 mouseWorldCoords = Main.MainCamera.TransformScreenCoordinates(Input.MousePosition);
			var distanceToMouse = Vector2.Distance(_transform.Position, mouseWorldCoords);
			var radius = Entity.GetComponent<CellScript>().Radius;

			if (distanceToMouse > radius)
			{
				Vector2 direction = mouseWorldCoords - _transform.Position;
				direction.Normalize();
				_transform.Position += direction * MovementSpeed * (float) deltaTime;

				_followScript.IsFollowing = false;
			}
			else
			{
				if (_transform == null || _followScript == null) return;

				_followScript.Target.Position = Main.MainCamera.TransformScreenCoordinates(Input.MousePosition);

				_followScript.IsFollowing = true;
			}
		}

		public void GuiDraw()
		{
			SpriteFont font = Resources.GetFont("Fonts\\Font");
//			var mousePos = string.Format("Mouse Position:({0:0.00},{1:0.00})", _followScript.Target.Position.X, _followScript.Target.Position.Y);
//
//			var thisPos = string.Format("Player Position:({0:0.00},{1:0.00})", _transform.Position.X, _transform.Position.Y);
//
//			Main.SpriteBatch.DrawString(font, mousePos, new Vector2(20, 20), Color.Black);

			var foodInstances = string.Format("Food instances:{0}", Food.FoodInstances);
			Main.SpriteBatch.DrawString(font, foodInstances, new Vector2(20, 20), Color.Black);
		}

	}
}
