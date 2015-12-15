using System;
using Microsoft.Xna.Framework;

namespace EatMe
{
	public static class HelperMethods
	{
		public static Random Rand { get; } = new Random();

		public static Color RandomColor()
		{
			return new Color(Rand.Next(255), Rand.Next(255), Rand.Next(255));
		}
	}
}
