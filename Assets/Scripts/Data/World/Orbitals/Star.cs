using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Star class defines a Star in a SolarSystem.
	/// </summary>
	public class Star : Orbital
	{
		public void GenerateStar(int numPlanets, int maxMoons)
		{
			OrbitalAngle = Point.RandomAngle;
			OrbitalDistance = 0;

			for (int i = 0; i < numPlanets; i++)
			{
				Planet planet = new Planet();
				planet.GeneratePlanet(i, maxMoons);
				this.AddChild(planet);
			}
		}
	}
}