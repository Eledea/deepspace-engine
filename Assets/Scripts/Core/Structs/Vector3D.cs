using System;
using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Defines a Vector with 3 double components.
	/// </summary>
	public struct Vector3D
	{
		//Constructors
		public Vector3D (double X, double Y, double Z) { x = X; y = Y; z = Z; }
		public Vector3D (double X, double Y) { x = X; y = Y; z = 0; }

		//Fields
		public double x;
		public double y;
		public double z;

		//Properties
		public static Vector3D back = new Vector3D(0, 0, -1);
		public static Vector3D down = new Vector3D (0, -1, 0);
		public static Vector3D forward = new Vector3D (0, 0, 1);
		public static Vector3D left = new Vector3D (-1, 0, 0);
		public static Vector3D one = new Vector3D (1, 1, 1);
		public static Vector3D right = new Vector3D (1, 0, 0);
		public static Vector3D up = new Vector3D (0, 1, 0);
		public static Vector3D zero = new Vector3D (0, 0, 0);

		public double magnitude {
			get { return Math.Sqrt (sqrMagnitude); }
		}

		public Vector3D normalized {
			get { return new Vector3D (x / magnitude, y / magnitude, z / magnitude); }
		}

		public double sqrMagnitude { 
			get { return x * x + y * y + z * z; }
		}

		//Functions
		public override int GetHashCode() {
			return ((byte)x << 16) | ((byte)y << 8) | (byte)z;
		}

		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				var rhs = obj as Vector3D?;
				if (rhs.HasValue)
					return this == rhs.Value;
			}

			return false;
		}

		public static double Distance(Vector3D a, Vector3D b)
		{
			return (a - b).magnitude;
		}

		public Vector3 ToVector3()
		{
			return new Vector3((float)x, (float)y, (float)z);
		}

		public static Vector3D ToVector3D(Vector3 input)
		{
			return new Vector3D(input.x, input.y, input.z);
		}

		public static Vector3D Clamp(Vector3D value, float min, float max)
		{
			return new Vector3D(Utility.ClampD(value.x, min, max), Utility.ClampD(value.y, min, max), Utility.ClampD(value.z, min, max));
		}

		//Operators
		public static bool operator == (Vector3D lhs, Vector3D rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}
		public static bool operator != (Vector3D lhs, Vector3D rhs)
		{
			return !(lhs == rhs);
		}

		public static Vector3D operator + (Vector3D v1, Vector3D v2) {
			return new Vector3D (v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
		}
		public static Vector3D operator + (Vector3D v1, Vector3 v2) {
			return new Vector3D (v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
		}

		public static Vector3D operator / (Vector3D v1, double d) {
			return new Vector3D (v1.x / d, v1.y / d, v1.z / d);
		}

		public static Vector3D operator * (Vector3D v1, double d) {
			return new Vector3D (v1.x * d, v1.y * d, v1.z * d);
		}
		public static Vector3D operator * (double d ,Vector3D v1) {
			return new Vector3D (v1.x * d, v1.y * d, v1.z * d);
		}

		public static Vector3D operator - (Vector3D v1, Vector3D v2) {
			return new Vector3D (v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}
		public static Vector3D operator - (Vector3D v1) {
			return new Vector3D(v1.x, v1.y, v1.z);
		}
	}
}