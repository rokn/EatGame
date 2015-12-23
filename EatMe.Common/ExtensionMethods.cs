using Microsoft.Xna.Framework;

namespace EatMe.Common
{
	public static class ExtensionMethods
	{

		public static Rectangle UpdateViaVector(this Rectangle rect, Vector2 vector)
		{
			rect.X = (int)vector.X;
			rect.Y = (int)vector.Y;

			return rect;
		}

		public static bool Contains(this Rectangle rect, Vector2 vector)
		{
			return rect.Left < vector.X && rect.Right > vector.X && rect.Top < vector.Y && rect.Bottom > vector.Y;
		}
	}
}
