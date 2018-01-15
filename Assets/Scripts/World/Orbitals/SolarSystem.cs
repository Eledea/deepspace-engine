using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sub-class of an Orbital for storing a SolarSystem.
/// </summary>
public class SolarSystem : Orbital
{
	/// <summary>
	/// Generates the data for the Orbitals in this SolarSystem.
	/// </summary>
	public void GenerateSolarSystem(int numPlanets, int maxMoons)
	{
		this.OrbitalDistance = 0;
		this.OrbitalAngle = UnityEngine.Random.Range (0, Mathf.PI * 2);

		for (int i = 0; i < numPlanets; i++)
		{
			Planet planet = new Planet ();
			planet.GeneratePlanet (i, maxMoons);
			this.AddChild (planet);
		}
	}
}