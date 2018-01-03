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
	public void Generate(int numOrbitals)
	{
		Orbitals = new List<Orbital>();

		for (int i = 0; i < numOrbitals; i++)
		{
			Orbital orbital = new Orbital ();

			Orbitals.Add (orbital);
		}
	}

	/// <summary>
	/// Load a SolarSystem with fileName.
	/// </summary>
	public void LoadFromFile(string fileName)
	{
	}

	public void Update(UInt64 timeSinceStart)
	{
		//TODO: Consider only updating part of the SolarSystem if we have a crazy
		//number of Orbitals?

		foreach (Orbital orbital in Orbitals)
		{
			orbital.Update (timeSinceStart);
		}
	}
}