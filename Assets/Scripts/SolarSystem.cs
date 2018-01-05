using System;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem
{
	/// <summary> The Orbitals in this SolarSystem. </summary>
	public List<Orbital> Orbitals;

	/// <summary>
	/// Generate the Orbitals for this SolarSystem.
	/// </summary>
	public void GenerateSolarSystem(int numPlanets, int minMoons, int maxMoons)
	{
		Orbitals = new List<Orbital> ();
		
		//Generate x number of planets.

		for (int i = 0; i < numPlanets; i++)
		{
			GeneratePlanet ();
		}
	}

	void GeneratePlanet()
	{
		Orbital orbital = new Orbital ();
		Orbitals.Add (orbital);

		orbital.Mu = 398600441800000;
		orbital.InitAngle = UnityEngine.Random.Range(0, Mathf.PI*2);
		orbital.OrbitalDistance = 149597870700;
		orbital.OrbitalPeriod = (Math.Sqrt((4 * Math.PI * Math.PI * Math.Pow(orbital.OrbitalDistance, 3)) / orbital.Mu)) / 1000000;
	}

	/// <summary>
	/// Updates each Orbital in this SolarSystem.
	/// </summary>
	public void UpdateSolarSystem(UInt64 timeSinceStart)
	{
		//TODO: Consider only updating part of the SolarSystem if we have a crazy
		//number of Orbitals?

		foreach (Orbital orbital in Orbitals)
		{
			orbital.Update (timeSinceStart);
		}
	}
}