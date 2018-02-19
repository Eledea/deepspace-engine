using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Contains fields that store data that can be sent to and interpreted by a Buildable.
	/// </summary>
	public struct BuildData
	{
		public string Name;
		public long Id;

		public Vector3D Position;
		public Quaternion Orientation;

		public BuildData(string name, long id, Vector3D position, Quaternion orientation)
		{
			Name = name; Id = id; Position = position; Orientation = orientation;
		}
	}
}