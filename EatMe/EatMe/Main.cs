using EatMe.Components;
using EatMe.Prefabs;
using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EatMe
{
	public class Main : Game
	{
		public GraphicsDeviceManager Graphics { get; }

		public Main()
		{
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			ContentManager = Content;

			World = new EntityWorld();
			
		}

		public static EntityWorld World { get; private set; }
		public static SpriteBatch SpriteBatch { get; private set; }
		public static ContentManager ContentManager { get; private set; }
		public static OrtographicCamera MainCamera { get; set; }

//		private Entity _player;


		private static void GenerateStartFood(int foodCount)
		{
			for (var i = 0; i < foodCount; i++)
			{
				Food.Instantiate(new Vector2(
					HelperMethods.Rand.Next(800),
					HelperMethods.Rand.Next(480)
					));
			}
		}

		protected override void Initialize()
		{
			Resources.Initialize();
			Player.GetInstance().AttachToWorld(World);
			Camera.GetInstance().AttachToWorld(World);
			Food.GetInstance().AttachToWorld(World);


			MainCamera = Camera.Instantiate(800,480).GetComponent<OrtographicCamera>();
			//			_player = Player.Instantiate("Default_Skin");
			//			_player.GetComponent<Transform>().Position = new Vector2(300, 240);
			GenerateStartFood(100);

			World.Start();
			base.Initialize();
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void UnloadContent()
		{
			World.Destroy();
		}

		protected override void Update(GameTime gameTime)
		{

			var deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
//			MainCamera.SmoothFollow(_player.GetComponent<Transform>(), 10f, deltaTime);

			World.Update(deltaTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null,null,null,null, MainCamera.TransformMatrix);
			World.Draw();
			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}