using UnityEngine;

namespace DeepSpace.Utility
{
	/// <summary>
	/// Contains functions for carrying out general utility tasks that are not class specific.
	/// </summary>
	public sealed class Utility
	{
		/// <summary>
		/// Returns a world space position from an array index.
		/// </summary>
		public static Vector3 IndexToWorldSpacePosition(int x, int y, int s, int a, int b)
		{
			return new Vector3 (x * s + s / 2 - a * s / 2, y * s + s / 2 - b * s / 2, 0);
		}

		/// <summary>
		/// Returns an array index from a world space position.
		/// </summary>
		public static Vector2 WorldSpacePositionToIndex(Vector3 position, int s, float a, float b)
		{
			return new Vector2(Mathf.FloorToInt((a * s / 2 + position.x) / s), Mathf.FloorToInt((b * s / 2 + position.y) / s));;
		}
	}
}