using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Planet class defines a Planet in a SolarSystem.
	/// </summary>
	public class Planet : Orbital
	{
		public void GeneratePlanet(int i, int maxMoons)
		{
			OrbitalAngle = Point.RandomAngle;
			OrbitalDistance = (ulong)(50000 * (i + 1));

			int numMoons = UnityEngine.Random.Range(0, maxMoons + 1);

			for (int m = 0; m < numMoons; m++)
			{
				Moon moon = new Moon();
				moon.GenerateMoon();
				this.AddChild(moon);
			}
		}
	}
}