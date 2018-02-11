namespace DeepSpace.Core
{
	/// <summary>
	/// Contains constants and methods for working with the DeepSpace Direction system.
	/// </summary>
	public sealed class BaseDirections
	{
		//TODO: Implement Direction system.

		public enum Axis : byte
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		public enum Direction : byte
		{
			Right = 0,
			Left = 1,
			Up = 2,
			Down = 3,
			Forward = 4,
			Backward = 5
		}
	}
}