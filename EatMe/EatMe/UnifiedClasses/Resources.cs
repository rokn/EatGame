using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace EatMe.UnifiedClasses
{
	public static class Resources
	{
		private static Dictionary<string, Texture2D> _textureCache;
		private static Dictionary<string, SpriteFont> _fontCache;


		public static void Initialize()
		{
			_textureCache = new Dictionary<string, Texture2D>();
			_fontCache = new Dictionary<string, SpriteFont>();
		}

		public static Texture2D GetTexture(string filename)
		{
			return GetResource(filename, _textureCache);
		}

		public static SpriteFont GetFont(string filename)
		{
			return GetResource(filename, _fontCache);
		}

		private static T LoadResource<T>(string filename)
		{
			return Main.ContentManager.Load<T>(filename);
		}

		public static T GetResource<T>(string filename, Dictionary<string, T> cache)
		{
			if(cache.ContainsKey(filename))
			{
				return cache[filename];
			}


			T newResource = LoadResource<T>(filename);
			cache.Add(filename, newResource);
			return newResource;
		} 


	}
}
