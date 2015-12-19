using System;
using Microsoft.Xna.Framework;

namespace EatMe.UnifiedClasses
{
	public static class HelperMethods
	{
		public static Random Rand { get; } = new Random();

		public static Color RandomColor()
		{
			return new Color(Rand.Next(255), Rand.Next(255), Rand.Next(255));
		}

		public static Rectangle UpdateViaVector(this Rectangle rect, Vector2 vector)
		{
			rect.X = (int)vector.X;
			rect.Y = (int)vector.Y;

			return rect;
		}
	}
}
