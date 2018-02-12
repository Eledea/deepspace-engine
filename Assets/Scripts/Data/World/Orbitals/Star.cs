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
			Name = "Star";
			OrbitalAngle = Point.RandomAngle;
			OrbitalDistance = 0;

			Transform = new MyEntityTransformComponent(this, Vector3D.zero, OrbitalPosition, Quaternion.identity);

			for (int i = 0; i < numPlanets; i++)
			{
				Planet planet = new Planet();
				planet.GeneratePlanet(i, maxMoons);
				this.AddChild(planet);
			}
		}
	}
}