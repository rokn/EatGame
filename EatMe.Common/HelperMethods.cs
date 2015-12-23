using System;
using Microsoft.Xna.Framework;

namespace EatMe.Common
{
	public static class HelperMethods
	{
		public static Random Rand { get; } = new Random();

		public static Color RandomColor()
		{
			return new Color(Rand.Next(255), Rand.Next(255), Rand.Next(255));
		}

		public static Vector2 GetRandomPositionInBounds()
		{
			//TODO: make bounds be independant on player start radius
			var startRadius = (int)Configuration.PlayerStartingRadius;

			float x = Rand.Next(startRadius, Configuration.WorldWidth - startRadius);
			float y = Rand.Next(startRadius, Configuration.WorldWidth - startRadius);

			return new Vector2(x, y);
		}
	}
}
