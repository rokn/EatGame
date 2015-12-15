using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EatMe
{
	public static class Resources
	{
		private static Dictionary<string, Texture2D> _textureCache;


		public static void Initialize()
		{
			_textureCache = new Dictionary<string, Texture2D>();
		}

		public static Texture2D GetTexture(string filename)
		{
			if(_textureCache.ContainsKey(filename))
			{
				return _textureCache[filename];
			}
			else
			{
				Texture2D newTexture = LoadTexture(filename);
				_textureCache.Add(filename, newTexture);
				return newTexture;
			}
		}

		private static Texture2D LoadTexture(string filename)
		{
			return Main.ContentManager.Load<Texture2D>(filename);
		}
	}
}
