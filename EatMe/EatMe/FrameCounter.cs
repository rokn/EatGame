using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EatMe
{
	public class FrameRateCounter : DrawableGameComponent
	{
		private SpriteBatch _spriteBatch;
		private static string _fps;
		private int _frameRate = 0;
		private int _frameCounter = 0;
		private TimeSpan _elapsedTime = TimeSpan.Zero;
		private SpriteFont _font;

		public FrameRateCounter(Game game)
			: base(game)
		{
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_font = Main.ContentManager.Load<SpriteFont>(@"Fonts\Font");
		}

		public override void Update(GameTime gameTime)
		{
			_elapsedTime += gameTime.ElapsedGameTime;

			if (_elapsedTime <= TimeSpan.FromSeconds(1)) return;


			_elapsedTime -= TimeSpan.FromSeconds(1);
			_frameRate = _frameCounter;
			_frameCounter = 0;
		}

		public override void Draw(GameTime gameTime)
		{
			_frameCounter++;
			_fps = string.Format("Fps: {0}", _frameRate);

			_spriteBatch.Begin();
			_spriteBatch.DrawString(_font, _fps, new Vector2(33, 100), Color.Black);
			_spriteBatch.DrawString(_font, _fps, new Vector2(32, 99), Color.White);

			_spriteBatch.End();
		}
	}
}