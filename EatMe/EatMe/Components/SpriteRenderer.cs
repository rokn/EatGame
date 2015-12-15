using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EatMe.Components
{
	public class SpriteRenderer : Component, IDrawableComponent
	{
		public int DrawLayer { get; set; }
		public Texture2D Texture { get; set; }
		public Vector2 Origin { get; set; }
		public Color Color { get; set; }
		public float Transperency { get; set; }

		public SpriteRenderer()
		{
			Texture = null;
			Color = Color.White;
			Transperency = 1.0f;
		}

		public SpriteRenderer(string filename)
		{
			Texture = Resources.GetTexture(filename);
			Origin = new Vector2(Texture.Width/2, Texture.Height/2);
			Color = Color.White;
			Transperency = 1.0f;
		}

		public void Draw()
		{
			Transform transform = Entity.GetComponent<Transform>();
			if(Texture != null && transform != null)
			{
				Main.SpriteBatch.Draw(Texture, transform.Position, null, Color * Transperency, (float)transform.Rotation, Origin, transform.Scale, SpriteEffects.None, 0.1f);
			}
		}
	}
}
