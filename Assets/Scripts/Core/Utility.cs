using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Contains functions for carrying out general utility tasks that are not class specific.
	/// </summary>
	public sealed class Utility
	{
		/// <summary>
		/// Returns the absolute value of a double.
		/// </summary>
		public static double Abs(double d)
		{
			if (d < 0)
				return -d;
			else
				return d;
		}

		/// <summary>
		/// Returns a random floating-point number representing an angle in radians around a point.
		/// </summary>
		public static float RandomizedPointAngle
		{
			get
			{
				return Random.Range(0, Mathf.PI * 2);
			}
		}

		/// <summary>
		/// Returns the center of a 1D line from it's length.
		/// </summary>
		/// <returns></returns>
		public static float SizeToCenter(int x)
		{
			return x / 2;
		}

		/// <summary>
		/// Returns 1 if true, 0 is false.
		/// </summary>
		public static int BoolToInt(bool b)
		{
			if (!b)
				return 0;
			else
				return 1;
		}

		/// <summary>
		/// Returns a world-space position from an array index.
		/// </summary>
		public static Vector2 IndexToWorldSpacePosition(int x, int y, int s, int a, int b)
		{
			return new Vector2(x * s + s / 2 - a * s / 2 * s, y * s + s / 2 - b * s / 2 * s);
		}
	}
}