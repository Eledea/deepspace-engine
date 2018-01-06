using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standalone Data class for storing a Galaxy.
/// </summary>
public class Galaxy
{
	/// <summary> The SolarSystems in this Galaxy. </summary>
	public List<SolarSystem> SolarSystems;

	/// <summary>
	/// Generate the SolarSystems for this Galaxy.
	/// </summary>
	public void GenerateGalaxy(int numSolarSystems)
	{
		SolarSystems = new List<SolarSystem>();

		for (int i = 0; i < numSolarSystems; i++)
		{
			SolarSystem ss = new SolarSystem ();
			ss.GenerateSolarSystem (4, 1);

			SolarSystems.Add (ss);
		}
	}

	/// <summary>
	/// Load a Galaxy with fileName.
	/// </summary>
	public void LoadFromFile(string fileName)
	{
	}

	/// <summary>
	/// Update each Orbital in this Galaxy.
	/// </summary>
	public void UpdateGalaxy(UInt64 timeSinceStart)
	{
		//TODO: Consider only updating part of the Galaxy if we have a crazy
		//number of SolarSystems?

		foreach (SolarSystem ss in this.SolarSystems)
		{
			ss.UpdateSolarSystem (timeSinceStart);
		}
	}
}