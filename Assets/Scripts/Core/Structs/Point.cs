namespace DeepSpace.Core
{
	/// <summary>
	/// Contains properties and methods for working with theoretical points.
	/// </summary>
	public struct Point
	{
		/// <summary>
		/// Returns a random floating-point number representing an angle in radians around a point.
		/// </summary>
		public static float RandomAngle
		{
			get
			{
				return UnityEngine.Random.Range(0, 6.28318548F);
			}
		}
	}
}