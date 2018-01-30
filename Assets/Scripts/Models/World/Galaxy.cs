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

	//TODO: Make this use the Player's current SolarSystem instead.

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

		//TODO; Wtf is this doing? Fix it asap!
		SolarSystemView.Instance.Player = PlayerManager.Instance.GetPlayerInManager (37331);

		SolarSystemView.Instance.SetSolarSystem ();
	}

	/// <summary>
	/// Sets the current SolarSystem we are showing.
	/// </summary>
	public void SetSolarSystem(int id)
	{
		CurrentSolarSystem = SolarSystems [id];
	}

	float advanceTimeTimer = 1f;

	/// <summary>
	/// Updates the time timer.
	/// </summary>
	public void UpdateTime()
	{
		advanceTimeTimer -= Time.deltaTime;

		if (advanceTimeTimer < 0)
			AdvanceTime ();
	}

	/// <summary>
	/// Advances the time in this Galaxy by 1 second.
	/// </summary>
	public void AdvanceTime()
	{
		timeSinceStart += (uint)1;
		advanceTimeTimer = 0f;
	}
}