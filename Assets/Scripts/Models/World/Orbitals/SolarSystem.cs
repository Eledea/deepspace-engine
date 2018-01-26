using DeepSpace.Utility;
using UnityEngine;

/// <summary>
/// Sub-class of an Orbital for storing a SolarSystem.
/// </summary>
public class SolarSystem : Orbital
{
	public void GenerateSolarSystem(int numPlanets, int maxMoons)
	{
		OrbitalAngle = Utility.RandomizePointAngle;
		OrbitalDistance = 0;

		for (int i = 0; i < numPlanets; i++)
		{
			Planet planet = new Planet ();
			planet.GeneratePlanet (i, maxMoons);
			this.AddChild (planet);
		}
	}
}