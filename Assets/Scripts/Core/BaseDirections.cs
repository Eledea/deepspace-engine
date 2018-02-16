using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Integer representation for each axis in the Cartesian co-ordinate system.
	/// </summary>
	public enum Axis : byte
	{
		X = 0,
		Y = 1,
		Z = 2
	}

	/// <summary>
	/// Integer representation for each dirction in the Cartesian co-ordinate system.
	/// </summary>
	public enum Direction : byte
	{
		Right = 0,
		Left = 1,
		Up = 2,
		Down = 3,
		Forward = 4,
		Backward = 5,
		Null = 6,
	}

	/// <summary>
	/// Contains constants and methods for working with the DeepSpace Direction system.
	/// </summary>
	public sealed class BaseDirections
	{
		/// <summary>
		/// Enumerable for working a collection of direction constants.
		/// </summary>
		[System.Flags]
		public enum DirectionFlags : byte
		{
			//TODO: Use this later for more advanced mechanics.

			Right		= 1 << (int)Direction.Right,
			Left		= 1 << (int)Direction.Left,
			Up			= 1 << (int)Direction.Up,
			Down		= 1 << (int)Direction.Left,
			Forward		= 1 << (int)Direction.Forward,
			Backward	= 1 << (int)Direction.Backward,

			All = Right | Left | Up | Down | Forward | Backward,
		}

		/// <summary>
		/// Vectors for direction constant with floaing-point number components.
		/// </summary>
		public static readonly Vector3[] Directions =
		{
			Vector3.right,
			Vector3.left,
			Vector3.up,
			Vector3.down,
			Vector3.forward,
			Vector3.back
		};

		/// <summary>
		/// Returns a vector from a direction 
		/// </summary>
		public static Vector3 GetVector(Direction dir)
		{
			return Directions[(int)dir];
		}

		/// <summary>
		/// Returns a 3D position vector from a Quaternion rotation and the desired direction.
		/// </summary>
		/// <param name="The rotation of the Entity."></param>
		/// <param name="The desired direction."></param>
		/// <returns></returns>
		public static Vector3 VectorInDirection(Quaternion rot, Direction dir)
		{
			return rot * GetVector(dir);
		}
	}
}