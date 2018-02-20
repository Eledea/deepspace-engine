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
			OrbitalAngle = Point.RandomAngle;
			OrbitalDistance = 10000;
		}
	}
}