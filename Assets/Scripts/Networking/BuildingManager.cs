using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace.Networking
{
	public class BuildingManager : MonoBehaviour
	{
		public static BuildingManager Instance { get; protected set; }

		void OnEnable()
		{
			Instance = this;
		}

		public void OnBuildableCreated(SolarSystem ss, Vector3D position, Quaternion rotation)
		{
			//TODO: Implement an Object factory to manage this.

			//For now just spawn a Storage.
			var b = new Storage("Storage", 47483, Vector3D.zero, position, rotation);
			ss.AddEntityToSolarSystem(b);
		}
	}
}