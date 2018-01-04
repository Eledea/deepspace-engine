using System;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem
{
	public SolarSystem()
	{
	}

	/// <summary> The Orbitals in this SolarSystem. </summary>
	public List<Orbital> Orbitals;

	/// <summary>
	/// Generate the Orbitals for this SolarSystem.
	/// </summary>
	public void GenerateSolarSystem(int numPlanets, int minMoons, int maxMoons)
	{
		Orbitals = new List<Orbital>();

		for (int i = 0; i < numPlanets; i++)
		{
			Orbital orbital = new Orbital ();
			orbital.Mu = (ulong)UnityEngine.Random.Range(10, 200) * 667408000 * (ulong)UnityEngine.Random.Range(10, 200);
			orbital.OrbitalDistance = (ulong)UnityEngine.Random.Range(1, 101) * (ulong)UnityEngine.Random.Range(1, 1001) * (ulong)1000000000000;
			orbital.OrbitalPeriod = (Math.Sqrt((4 * Math.PI * Math.PI * Math.Pow(orbital.OrbitalDistance, 3)) / orbital.Mu));
			orbital.InitAngle = UnityEngine.Random.Range(0, Mathf.PI*2);

			Orbitals.Add (orbital);
		}
	}

	public void UpdateSolarSystem(UInt64 timeSinceStart)
	{
		//TODO: Consider only updating part of the SolarSystem if we have a crazy
		//number of Orbitals?

		foreach (Orbital orbital in this.Orbitals)
		{
			orbital.Update (timeSinceStart);
		}
	}
}