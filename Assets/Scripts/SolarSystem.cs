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
	public void GenerateSolarSystem(int numSolarSystems, int numPlanets, int maxMoons, int solarSystemID)
	{
		Orbital star = new Orbital ();
		this.AddChild (star);

		//TODO: We need a better way of defining the properties of an Orbital.

		star.Name = "Star";
		star.Type = 1;
		star.Mu = 13271244001800000000;
		star.OrbitalDistance = 598391482800;
		star.InitAngle = (Mathf.PI * 2) / numSolarSystems * solarSystemID;
		star.OrbitalPeriod = (Math.Sqrt((4 * Math.PI * Math.PI * Math.Pow(star.OrbitalDistance, 3)) / star.Mu) * 10) / 100000;

		//Generate x number of planets.

		for (int i = 0; i < numPlanets; i++)
		{
			Planet planet = new Planet ();
			star.AddChild (planet);
			planet.GeneratePlanet (maxMoons);
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