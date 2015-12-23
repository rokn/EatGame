using EatMe.Prefabs;
using EatMe.UnifiedClasses;
using EatMe.Common;
using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EatMe.Components
{
	public class PlayerController : Component, IUpdateableComponent, IGuiDrawableComponent
	{
		public int GuiDrawLayer{ get; set; }

		public float MovementSpeed { get; set; }

		private Transform _transform;
		private SmoothFollowScript _followScript;

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();
			_followScript = Entity.GetComponent<SmoothFollowScript>();
		}

		//TODO: simplify update
		public void Update(double deltaTime)
		{
			MovementSpeed = Configuration.PlayerBaseMovementSpeed - Entity.GetComponent<CellScript>().Radius;

			Vector2 mouseWorldCoords = Main.MainCamera.TransformScreenCoordinates(Input.MousePosition);
			var distanceToMouse = Vector2.Distance(_transform.Position, mouseWorldCoords);
			var radius = Entity.GetComponent<CellScript>().Radius;

			if (distanceToMouse > radius)
			{
				Vector2 direction = mouseWorldCoords - _transform.Position;
				direction.Normalize();
				_transform.Position += direction * MovementSpeed * (float) deltaTime;
			}
			else
			{
				if (_transform == null || _followScript == null) return;

				_followScript.Target.Position = Main.MainCamera.TransformScreenCoordinates(Input.MousePosition);
			}

			KeepInBounds();
		}

		public void GuiDraw()
		{
			SpriteFont font = Resources.GetFont(Configuration.FontsFolder + "Font");
//			var mousePos = string.Format("Mouse Position:({0:0.00},{1:0.00})", _followScript.Target.Position.X, _followScript.Target.Position.Y);
//
			var thisPos = string.Format("Player Position:({0:0.00},{1:0.00})", _transform.Position.X, _transform.Position.Y);

			var rect = Main.MainCamera._viewRectangle.ToString();

			var drawCalls = string.Format("Draw calls: {0}", World.DrawCalls);
//
//			Main.SpriteBatch.DrawString(font, mousePos, new Vector2(20, 20), Color.Black);

			var foodInstances = string.Format("Food instances:{0}", Food.FoodInstances);

			Main.SpriteBatch.DrawString(font, foodInstances, new Vector2(20, 20), Color.Black);

			Main.SpriteBatch.DrawString(font, thisPos, new Vector2(20, 40), Color.Black);

			Main.SpriteBatch.DrawString(font, rect, new Vector2(20, 60), Color.Black);

			Main.SpriteBatch.DrawString(font, drawCalls, new Vector2(20, 80), Color.Black);
		}

		private void KeepInBounds()
		{
			var radius = Entity.GetComponent<CellScript>().Radius;

			if (_transform == null) return;

			if(_transform.Position.X - radius < 0)
			{
				_transform.Position = new Vector2(radius, _transform.Position.Y);
			}
			else if(_transform.Position.X + radius > Configuration.WorldWidth)
			{
				_transform.Position = new Vector2(Configuration.WorldWidth - radius, _transform.Position.Y);
			}

			if(_transform.Position.Y - radius < 0)
			{
				_transform.Position = new Vector2(_transform.Position.X, radius);
			}
			else if(_transform.Position.Y + radius > Configuration.WorldHeight)
			{
				_transform.Position = new Vector2(_transform.Position.X, Configuration.WorldHeight - radius);
			}
		}
	}
}
