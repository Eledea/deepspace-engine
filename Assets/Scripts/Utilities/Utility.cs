using UnityEngine;

namespace DeepSpace.Utility
{
	/// <summary>
	/// Contains functions for carrying out general utility tasks that are not class specific.
	/// </summary>
	public sealed class Utility
	{
		/// <summary>
		/// Returns an array index from a world space position.
		/// </summary>
		public static Vector2 WorldSpacePositionToIndex(Vector2 position, int s, int a, int b)
		{
			Vector2 index = new Vector2(Mathf.FloorToInt((a * s / 2 + position.x) / s), Mathf.FloorToInt((b * s / 2 + position.y) / s));

			if (index.x < 0 || index.x >= a || index.y < 0 || index.y >= b)
				return -Vector2.one;
			else
				return index;
		}

		/// <summary>
		/// Returns a world space position from an array index.
		/// </summary>
		public static Vector2 IndexToWorldSpacePosition(int x, int y, int s, int a, int b)
		{
			if (x < a || y < b)
				return new Vector2 (x * s + s / 2 - a * s / 2, y * s + s / 2 - b * s / 2);
			else
				return Vector2.zero;
		}
	}
}