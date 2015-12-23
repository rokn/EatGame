using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using EatMe.Common;
using EatMe.Components;
using EatMe.Prefabs;
using EatMe.UnifiedClasses;
using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using EatMeServer;

namespace EatMe
{
	public class Main : Game
	{
		#region Constructor

		public Main()
		{
			Configuration.Load(Directory.GetCurrentDirectory() + "\\config.ini");

			Client = new MeClient("localhost", 14242, "EatMe");
			Client.FoodGenerateEvent += Client_FoodGenerateEvent;
			Client.Connect("Rokner");

			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			ContentManager = Content;
			Components.Add(new FrameRateCounter(this));


			ManageFullScreen();
			IsMouseVisible = true;

			World = new EntityWorld();

			
		}

		private static void Client_FoodGenerateEvent(List<Vector2> food)
		{
			foreach (var vector2 in food)
			{
				Food.Instantiate(vector2);
			}
		}

		#endregion

		#region Properties

		public static MeClient Client;
		public GraphicsDeviceManager Graphics{ get; }
		public static EntityWorld World { get; private set; }
		public static SpriteBatch SpriteBatch { get; private set; }
		public static ContentManager ContentManager { get; private set; }
		public static OrtographicCamera MainCamera { get; set; }
		public static int WindowWidth { get; set; }
		public static int WindowHeight { get; set; }

		#endregion

		private Entity _player;

		#region Initialization

		protected override void Initialize()
		{
			//Init static classes
			Resources.Initialize();
			Input.Initialize(WindowWidth, WindowHeight, 100.0f);

			//Init prefabs
			Player.GetPrefab().AttachToWorld(World);
			Camera.GetPrefab().AttachToWorld(World);
			Food.GetPrefab().AttachToWorld(World);

			//Master object init
			Entity masterObject = new Entity();
			masterObject.AttachComponent(new CollisionChecker());
			masterObject.Tag = "Master";
			World.AddEntity(masterObject);

			//Init a camera
			Entity camera = Camera.Instantiate(WindowWidth, WindowHeight);

			//Init a player
			for (var i = 0; i < 50; i++)
			{
				Player.Instantiate("Skin_00_0");
			}

			_player = Player.Instantiate("Skin_01_0");
			_player.AttachComponent(new PlayerController());


			//Init Main camera
			MainCamera = camera.GetComponent<OrtographicCamera>();
			camera.AttachComponent(new SmoothFollowScript(_player.GetComponent<Transform>())
			{
				SmoothTime = Configuration.CameraSmoothSpeed
			});

			camera.GetComponent<Transform>().SetScale(1.0f);

			//TODO: Why u wont work???
//			camera.GetComponent<Transform>().Position = _player.GetComponent<Transform>().Position;

			World.Start();
			base.Initialize();
		}

		#endregion

		#region Resource Managment

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void UnloadContent()
		{
			World.Destroy();
		}

		#endregion

		protected override void Update(GameTime gameTime)
		{

			Client.CheckForMessages();

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

		#region Fullscreen Managment

		private void GoFullscreenBorderless()
		{
			IntPtr hWnd = Window.Handle;
			var control = Control.FromHandle(hWnd);
			var form = control.FindForm();
			if (form != null)
			{
				form.FormBorderStyle = FormBorderStyle.None;
				form.WindowState = FormWindowState.Maximized;
			}

			WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
		}

		private void SwitchFullscreen(bool isFullscreen)
		{
			Graphics.IsFullScreen = isFullscreen;

			if (Configuration.ResolutionIsValid)
			{
				Graphics.PreferredBackBufferWidth = Configuration.ResolutionWidth;
				Graphics.PreferredBackBufferHeight = Configuration.ResolutionHeight;
			}
			else
			{
				Graphics.PreferredBackBufferWidth = Configuration.DefaultResolutionWidth;
				Graphics.PreferredBackBufferHeight = Configuration.DefaultResolutionHeight;
			}

			WindowWidth = Graphics.PreferredBackBufferWidth;
			WindowHeight = Graphics.PreferredBackBufferHeight;
		}

		private void ManageFullScreen()
		{
			switch (Configuration.FullScreenMode)
			{
				case 0:
					SwitchFullscreen(false);
					break;
				case 1:
					GoFullscreenBorderless();
					break;
				case 2:
					SwitchFullscreen(true);
					break;
			}
		}

		#endregion

		private static bool CheckForExit()
		{
			return Input.KeyJustPressed(Keys.Escape);
		}
	}
}