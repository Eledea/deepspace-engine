using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Moon class defines a Moon in a SolarSystem.
	/// </summary>
	public class Moon : Orbital
	{
		public void GenerateMoon()
		{
			Name = "Moon";
			OrbitalAngle = Point.RandomAngle;
			OrbitalDistance = 10000;

			Transform = new MyEntityTransformComponent(Vector3D.zero, OrbitalPosition, Quaternion.identity);
		}
	}
}