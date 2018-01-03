using System.Collections.Generic;
using UnityEngine;

public class SolarSystem
{
	public SolarSystem()
	{
	}

	/// <summary>
	/// The Orbitals in this SolarSystem.
	/// </summary>
	public List<Orbital> Orbitals;

	/// <summary>
	/// Generate the Orbitals for this Solar System.
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

	public void LoadFromFile(string fileName)
	{
	}
}