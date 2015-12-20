using INIFiles;

namespace EatMe
{
	public static class Configuration
	{
		public const int DefaultResolutionWidth = 800;
		public const int DefaultResolutionHeight = 600;

		public static float PlayerStartingRadius { get; private set; }
		public static int PlayerMinDrawLayer { get; private set; }
		public static int PlayerMaxDrawLayer { get; private set; }
		public static float CellEatDiffrence { get; private set; }
		public static float CameraSmoothSpeed { get; private set; }
		public static string SpritesFolder { get; private set; }
		public static int FullScreenMode { get; private set; }
		public static int ResolutionWidth { get; private set; }
		public static int ResolutionHeight { get; private set; }
		public static bool ResolutionIsValid { get; private set; }

		public static void Load(string configFile)
		{
			IniFile file = new IniFile(configFile);

			PlayerStartingRadius = float.Parse(file.IniReadValue("Player", "StartRadius", "30"));

			PlayerMinDrawLayer = int.Parse(file.IniReadValue("Player", "MinDrawLayer", "1"));

			PlayerMaxDrawLayer = int.Parse(file.IniReadValue("Player", "MaxDrawLayer", "1000"));

			CellEatDiffrence = float.Parse(file.IniReadValue("Cell", "EatDiff", "0.9"));

			CameraSmoothSpeed = float.Parse(file.IniReadValue("Camera", "SmoothSpeed", "10"));

			SpritesFolder = file.IniReadValue("Config", "SpritesFolder", "Sprites\\");

			FullScreenMode = int.Parse(file.IniReadValue("Config", "FullScreenMode", "1"));

			ResolutionWidth = int.Parse(file.IniReadValue("Config", "ResolutionWidth", "0"));

			ResolutionHeight = int.Parse(file.IniReadValue("Config", "ResolutionHeight", "0"));

			ResolutionIsValid = ValidateResolution();
		}

		private static bool ValidateResolution()
		{
			return ResolutionWidth > DefaultResolutionWidth && ResolutionHeight > DefaultResolutionHeight;
		}
	}
}
