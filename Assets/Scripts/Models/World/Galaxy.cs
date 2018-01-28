using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Independent data class for storing a Galaxy.
/// </summary>
public class Galaxy
{
	public Galaxy()
	{
		SolarSystems = new List<SolarSystem> ();
	}

	/// <summary>
	/// The elapsed time in game since Galaxy creation in seconds.
	/// </summary>
	public UInt64 timeSinceStart { get; set; }

	/// <summary>
	/// The SolarSystems in this Galaxy.
	/// </summary>
	public List<SolarSystem> SolarSystems { get; set; }

	/// <summary>
	/// The SolarSystem we are currently showing.
	/// </summary>
	public SolarSystem CurrentSolarSystem { get; set; }

	/// <summary>
	/// Generate a new galaxy with number of solar systems.
	/// </summary>
	public void GenerateGalaxy(int numSolarSystems, int numPlanets, int maxMoons)
	{
		for (int i = 0; i < numSolarSystems; i++)
		{
			SolarSystem ss = new SolarSystem ();
			ss.GenerateSolarSystem (numPlanets, maxMoons);

			SolarSystems.Add (ss);
		}

		CurrentSolarSystem = SolarSystems [0];

		PlayerManager.Instance.CreatePlayerInManager (CurrentSolarSystem);
	}

	/// <summary>
	/// Sets the current SolarSystem we are showing.
	/// </summary>
	public void SetSolarSystem(int id)
	{
		CurrentSolarSystem = SolarSystems [id];
	}

	/// <summary>
	/// Advances game time by a number of seconds.
	/// </summary>
	void AdvanceTime(int numSeconds)
	{
		//TODO: Make this work in REAL seconds.

		timeSinceStart += (uint)numSeconds;
	}

	/// <summary>
	/// Updates all the SolarSystem in this Galaxy.
	/// </summary>
	public void UpdateGalaxy()
	{
		AdvanceTime (1);
	}
}