using System;
using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Defines a Vector with 2 integer components.
	/// </summary>
	public struct Vector2I
	{
		//Constructors
		public Vector2I (int X, int Y) { x = X; y = Y; }
		public Vector2I (float X, float Y) { x = Mathf.FloorToInt(X); y = Mathf.FloorToInt(Y); }

		//Fields
		public int x;
		public int y;

		//Properties
		public static Vector2I down = new Vector2I(0, -1);
		public static Vector2I left = new Vector2I(-1, 0);
		public static Vector2I one = new Vector2I(1, 1);
		public static Vector2I right = new Vector2I(0, 1);
		public static Vector2I up = new Vector2I(0, 1);
		public static Vector2I zero = new Vector2I(0, 0);

		//Functions
		public override int GetHashCode()
		{
			return ((byte)x << 16) | ((byte)y << 8);
		}

		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				var rhs = obj as Vector2I?;
				if (rhs.HasValue)
					return this == rhs.Value;
			}

			return false;
		}

		//Operators
		public static bool operator ==(Vector2I v1, Vector2I v2)
		{
			return v1.x == v2.x && v1.y == v2.y;
		}
		public static bool operator !=(Vector2I v1, Vector2I v2)
		{
			return !(v1 == v2);
		}
	}
}