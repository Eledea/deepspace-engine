using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Integer representation for each dirction in the Cartesian co-ordinate system.
	/// </summary>
	public enum Direction : byte
	{
		Forward		= 0,
		Backward	= 1,
		Left		= 2,
		Right		= 3,
		Up			= 4,
		Down		= 5,
	}

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

			Forward		= 1 << (int)Direction.Forward,
			Backward	= 1 << (int)Direction.Backward,
			Left		= 1 << (int)Direction.Left,
			Right		= 1 << (int)Direction.Right,
			Up			= 1 << (int)Direction.Up,
			Down		= 1 << (int)Direction.Down,

			All = Forward | Backward | Left | Right | Up | Down
		}

		/// <summary>
		/// Vectors for direction constant with floaing-point number components.
		/// </summary>
		private static readonly Vector3[] Directions =
		{
			Vector3.forward,
			Vector3.back,
			Vector3.left,
			Vector3.right,
			Vector3.up,
			Vector3.down
		};

		/// <summary>
		/// Vectors for direction constant with floaing-point number components.
		/// </summary>
		private static readonly Quaternion[] Rotations =
		{
			Quaternion.Euler(0, 0, 0),
			Quaternion.Euler(0, 180, 0),
			Quaternion.Euler(0, 90, 0),
			Quaternion.Euler(0, 270, 0),
			Quaternion.Euler(0, 0, 90),
			Quaternion.Euler(0, 0, 270)
		};

		/// <summary>
		/// Returns a vector from a direction.
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

		/// <summary>
		/// Returns a Rotation facing a desired direction.
		/// </summary>
		/// <param name="The direction to rotate to."></param>
		/// <returns></returns>
		public static Quaternion GetRotation(Direction dir)
		{
			return Rotations[(int)dir];
		}

		/// <summary>
		/// Returns the next Direction in the enumerable.
		/// </summary>
		/// <param name="The current direction."></param>
		/// <returns></returns>
		public static Direction GetNextDirection(Direction dir)
		{
			int d = (int)dir;

			if (d < 5)
				return (Direction)d + 1;

			return Direction.Forward;
		}

		/// <summary>
		/// Returns the previous Direction in the enumerable.
		/// </summary>
		/// <param name="The current direction."></param>
		/// <returns></returns>
		public static Direction GetPrevDirection(Direction dir)
		{
			int d = (int)dir;

			if (d > 0)
				return (Direction)d - 1;

			return Direction.Down;
		}

		/// <summary>
		/// Returns the Axis of the given Direction.
		/// </summary>
		/// <param name="The direction to get axis of."></param>
		/// <returns></returns>
		public static Axis GetAxis(Direction dir)
		{
			if (dir == Direction.Left || dir == Direction.Right)
				return Axis.X;
			if (dir == Direction.Up || dir == Direction.Down)
				return Axis.Y;
			else
				return Axis.Z;
		}
	}
}