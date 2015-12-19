using System;
using EatMe.Components;
using EatMe.Prefabs;
using EatMe.UnifiedClasses;
using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EatMe
{
	public class Main : Game
	{
		public GraphicsDeviceManager Graphics{ get; }

		public Main()
		{
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			ContentManager = Content;
			GoFullscreenBorderless();
			IsMouseVisible = true;

			World = new EntityWorld();
				
		}

		public static EntityWorld World{ get; private set; }
		public static SpriteBatch SpriteBatch{ get; private set; }
		public static ContentManager ContentManager{ get; private set; }
		public static OrtographicCamera MainCamera{ get; set; }
		public static int WindowWidth { get; set; }
		public static int WindowHeight { get; set; }

		private Entity _player;

		private static void GenerateStartFood(int foodCount)
		{
			for(var i = 0; i < foodCount; i++)
			{
				Food.Instantiate(new Vector2(
					HelperMethods.Rand.Next(-WindowWidth/2, WindowWidth/2),
					HelperMethods.Rand.Next(-WindowHeight/2, WindowHeight/2)
					));
			}
		}

		protected override void Initialize()
		{
			//Init static classes
			Resources.Initialize();
			Input.Initialize(WindowWidth,WindowHeight, 100.0f);

			//Init prefabs
			Player.GetInstance().AttachToWorld(World);
			Camera.GetInstance().AttachToWorld(World);
			Food.GetInstance().AttachToWorld(World);

			//Master object init
			Entity masterObject = new Entity();
			masterObject.AttachComponent(new CollisionChecker());
			World.AddEntity(masterObject);

			//Init a camera
			Entity camera = Camera.Instantiate(WindowWidth, WindowHeight);

			//Init a player
			for (var i = 0; i < 2; i++)
			{
				Player.Instantiate(new Vector2(500, 500 + i * 90), "Skin_00_0");
			}
			Player.Instantiate(new Vector2(500, 500), "Skin_00_0");

			Player.Instantiate(new Vector2(500, 500), "Skin_00_0");
			_player = Player.Instantiate(new Vector2(), "Skin_00_0");
			_player.AttachComponent(new SmoothFollowScript() {SmoothTime = 3});
			_player.AttachComponent(new PlayerController());

//			player2.AttachComponent(new SmoothFollowScript() { SmoothTime = 3 });
//			player2.AttachComponent(new PlayerController()
//			{
//				MovementSpeed = 400
//			});


			GenerateStartFood(100);

			//Init Main camera
			MainCamera = camera.GetComponent<OrtographicCamera>();
			camera.AttachComponent(new SmoothFollowScript(_player.GetComponent<Transform>())
			{
				SmoothTime = 10
			});

			camera.GetComponent<Transform>().SetScale(1.0f);

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

			Input.Update(gameTime);

			if(CheckForExit())
				Exit();

			World.Update(deltaTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, MainCamera.TransformMatrix);
			World.Draw();
			SpriteBatch.End();

			SpriteBatch.Begin();
			World.GuiDraw();
			SpriteBatch.End();

			base.Draw(gameTime);
		}

		private void GoFullscreenBorderless()
		{
			IntPtr hWnd = Window.Handle;
			var control = System.Windows.Forms.Control.FromHandle(hWnd);
			var form = control.FindForm();
			if (form != null)
			{
				form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
				form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			}

			WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
		}

		private static bool CheckForExit()
		{
			return Input.KeyJustPressed(Keys.Escape);

		}
	}
}