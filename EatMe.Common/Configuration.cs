namespace EatMe.Common
{
	public static class Configuration
	{
		public const int DefaultResolutionWidth = 800;
		public const int DefaultResolutionHeight = 600;

		public static float PlayerStartingRadius { get; private set; }
		public static float PlayerBaseMovementSpeed { get; private set; }
		public static int PlayerMinDrawLayer { get; private set; }
		public static int PlayerMaxDrawLayer { get; private set; }
		public static float CellEatDiffrence { get; private set; }
		public static float CameraSmoothSpeed { get; private set; }
		public static string SpritesFolder { get; private set; }
		public static string FontsFolder { get; private set; }
		public static int FullScreenMode { get; private set; }
		public static int ResolutionWidth { get; private set; }
		public static int ResolutionHeight { get; private set; }
		public static bool ResolutionIsValid { get; private set; }
		public static int WorldWidth { get; private set; }
		public static int WorldHeight { get; private set; }
		public static int WorldFoodCount { get; private set; }

		public static void Load(string configFile)
		{
			IniFile file = new IniFile(configFile);

			PlayerStartingRadius = float.Parse(file.IniReadValue("Player", "StartRadius", "30"));
			PlayerMinDrawLayer = int.Parse(file.IniReadValue("Player", "MinDrawLayer", "1"));
			PlayerMaxDrawLayer = int.Parse(file.IniReadValue("Player", "MaxDrawLayer", "1000"));
			PlayerBaseMovementSpeed = float.Parse(file.IniReadValue("Player", "BaseMovementSpeed", "400"));


			CellEatDiffrence = float.Parse(file.IniReadValue("Cell", "EatDiff", "0.9"));

			CameraSmoothSpeed = float.Parse(file.IniReadValue("Camera", "SmoothSpeed", "10"));

			SpritesFolder = file.IniReadValue("Config", "SpritesFolder", "Sprites\\");
			FontsFolder = file.IniReadValue("Config", "FontsFolder", "Fonts\\");
			FullScreenMode = int.Parse(file.IniReadValue("Config", "FullScreenMode", "1"));
			ResolutionWidth = int.Parse(file.IniReadValue("Config", "ResolutionWidth", "0"));
			ResolutionHeight = int.Parse(file.IniReadValue("Config", "ResolutionHeight", "0"));
			ResolutionIsValid = ValidateResolution();

			WorldWidth = int.Parse(file.IniReadValue("World", "Width", "3000"));
			WorldHeight = int.Parse(file.IniReadValue("World", "Height", "3000"));
			WorldFoodCount = int.Parse(file.IniReadValue("World", "FoodCount", "1000"));
		}

		private static bool ValidateResolution()
		{
			return ResolutionWidth > DefaultResolutionWidth && ResolutionHeight > DefaultResolutionHeight;
		}
	}
}
