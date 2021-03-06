﻿using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary> 
	/// Contains functions for carrying out general Utility tasks.
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
		/// Returns a double clamped to a minimum and maximum.
		/// </summary>
		public static double ClampD(double value, double min, double max)
		{
			if (value < min)
				value = min;
			if (value > max)
				value = max;

			return value;
		}

		/// <summary>
		/// Returns 0 if false, 1 is true.
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
		public static Vector3 IndexToWorldSpacePosition(int x, int y, int s, int a, int b)
		{
			return new Vector3(x * s + s / 2 - a * s / 2, y * s + s / 2 - b * s / 2, 0);
		}
	}
}