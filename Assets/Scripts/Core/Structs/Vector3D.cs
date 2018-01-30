using System;
using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// DeepSpace engine struct dor working with doubles in the same way as Unity's Vector3. 
	/// </summary>
	public struct Vector3D
	{
		//Constructors
		public Vector3D (double X, double Y, double Z) { x = X; y = Y; z = Z; }

		public Vector3D (double X, double Y) { x = X; y = Y; z = 0; }

		//Components
		public double x;

		public double y;

		public double z;

		//Properties.
		public static Vector3D back {
			get { return new Vector3D (0, 0, -1); }
		}

		public static Vector3D down {
			get { return new Vector3D (0, -1, 0); }
		}

		public static Vector3D forward {
			get { return new Vector3D (0, 0, 1); }
		}

		public static Vector3D left {
			get { return new Vector3D (-1, 0, 0); }
		}

		public static Vector3D right {
			get { return new Vector3D (1, 0, 0); }
		}

		public static Vector3D up {
			get { return new Vector3D (0, 1, 0); }
		}

		public static Vector3D zero {
			get { return  new Vector3D (0, 0, 0); }
		}

		public double magnitude
		{
			get { return Math.Sqrt (sqrMagnitude); }
		}

		public Vector3D normalized
		{
			get { return new Vector3D (x / magnitude, y / magnitude, z / magnitude); }
		}

		public double sqrMagnitude
		{ 
			get { return x * x + y * y + z * z; }
		}

		//Methods
		public Vector3 ToVector3()
		{
			return new Vector3 ((float)x, (float)y, (float)z);
		}

		//Operators
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
	}
}