using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Contains fields that store data that can be sent to and interpreted by a Buildable.
	/// </summary>
	public struct BuildData
	{
		public MyEntityDefinitionId Definition;
		public long Id;

		public Vector3D Position;
		public Quaternion Orientation;

		public BuildData(MyEntityDefinitionId definition, long id, Vector3D position, Quaternion orientation)
		{
			Definition = definition; Id = id; Position = position; Orientation = orientation;
		}
	}
}