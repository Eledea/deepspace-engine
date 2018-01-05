using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sub-class of Orbital for a SolarSystem.
/// </summary>
public class SolarSystem : Orbital
{
	/// <summary>
	/// Generate the Orbitals for this SolarSystem.
	/// </summary>
	public void GenerateSolarSystem(int numPlanets, int minMoons, int maxMoons)
	{
		Orbital star = new Orbital ();
		this.AddChild (star);

		star.Mu = 13271244001800000000;
		star.OrbitalDistance = 1;
		star.InitAngle = UnityEngine.Random.Range(0, Mathf.PI*2);
		star.OrbitalPeriod = (Math.Sqrt((4 * Math.PI * Math.PI * Math.Pow(star.OrbitalDistance, 3)) / star.Mu) * 10);

		//Generate x number of planets.

		for (int i = 0; i < numPlanets; i++)
		{
			Planet planet = new Planet ();
			star.AddChild (planet);
			planet.GeneratePlanet ();
		}
	}

	/// <summary>
	/// Updates each Orbital in this SolarSystem.
	/// </summary>
	public void UpdateSolarSystem(UInt64 timeSinceStart)
	{
		//TODO: Consider only updating part of the SolarSystem if we have a crazy
		//number of Orbitals?

		foreach (Orbital orbital in this.Children)
		{
			orbital.Update (timeSinceStart);
		}
	}
}