using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Contains fields that store data about a BuildRequest that can be sent to and interpreted by a server.
	/// </summary>
	public struct BuildRequest
	{
		public MyEntityDefinitionId? Definition;

		public SolarSystem SolarSystem;
		public Vector3D Position;
		public Quaternion Orientation;

		public BuildCheckResult BuildCheckResult;

		public BuildRequest(MyEntityDefinitionId? definition, SolarSystem ss, Vector3D position, Quaternion orientation, BuildCheckResult result)
		{
			Definition = definition; SolarSystem = ss; Position = position; Orientation = orientation; BuildCheckResult = result;
		}
	}
}